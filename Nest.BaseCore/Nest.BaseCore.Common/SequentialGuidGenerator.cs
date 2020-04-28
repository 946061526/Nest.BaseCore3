using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Nest.BaseCore.Common
{

    /*
   Database                GUID Column             SequentialGuidType Value

   Microsoft SQL Server    uniqueidentifier        SequentialAtEnd
   MySQL                   char (36) 	            SequentialAsString
   Oracle                  raw(16)                 SequentialAsBinary
   PostgreSQL              uuid                    SequentialAsString
   SQLite                  varies                  varies
   */

    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum SequentialGuidType
    {
        /// <summary>
        /// MySQL、PostgreSQL
        /// </summary>
        SequentialAsString,
        /// <summary>
        /// Oracle
        /// </summary>
        SequentialAsBinary,
        /// <summary>
        /// SQL Server、
        /// </summary>
        SequentialAtEnd
    }

    /// <summary>
    /// 生成序列Guid
    /// </summary>
    public static class SequentialGuidGenerator
    {
        private static readonly RNGCryptoServiceProvider _rng = new RNGCryptoServiceProvider();

        public static Guid NewSequentialGuid(SequentialGuidType guidType)
        {
            byte[] randomBytes = new byte[10];
            _rng.GetBytes(randomBytes);

            long timestamp = DateTime.UtcNow.Ticks / 10000L;
            byte[] timestampBytes = BitConverter.GetBytes(timestamp);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(timestampBytes);
            }

            byte[] guidBytes = new byte[16];

            switch (guidType)
            {
                case SequentialGuidType.SequentialAsString:
                case SequentialGuidType.SequentialAsBinary:
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 0, 6);
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 6, 10);

                    // If formatting as a string, we have to reverse the order
                    // of the Data1 and Data2 blocks on little-endian systems.
                    if (guidType == SequentialGuidType.SequentialAsString && BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(guidBytes, 0, 4);
                        Array.Reverse(guidBytes, 4, 2);
                    }
                    break;

                case SequentialGuidType.SequentialAtEnd:
                    Buffer.BlockCopy(randomBytes, 0, guidBytes, 0, 10);
                    Buffer.BlockCopy(timestampBytes, 2, guidBytes, 10, 6);
                    break;
            }

            return new Guid(guidBytes);
        }
    }
}
