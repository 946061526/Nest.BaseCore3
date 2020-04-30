layui.define(['jquery', 'table', 'util'], function (exports) {
    var $ = layui.jquery;
    var table = layui.table;
    var util = layui.util;
    var ExportData = {};
    ExportData.Export = function (options) {
        var setting_options = options;
        var defaultWhere = { pageIndex: 1, pageSize: 100000 };

        if (typeof (setting_options.defaultWhere) != "undefined") {
            defaultWhere=setting_options.defaultWhere;
        }
        if (typeof (setting_options.exportDataType) == "undefined") {
            setting_options.exportDataType = "xls";   //cvs
        }
        if (typeof (setting_options.url) == "undefined") {
            setting_options.url = setting_options.toolbarObj.config.url;
        }
        if (typeof (setting_options.tableId) == "undefined") {
            setting_options.tableId = setting_options.toolbarObj.config.id;
        }
        if (typeof (setting_options.ajaxType) == "undefined") {
            setting_options.ajaxType = setting_options.toolbarObj.config.method;
        }
        if (typeof (setting_options.isLayuiExport) == "undefined") {
            setting_options.isLayuiExport = 0;
        }
        setting_options.exportWhere = $.extend({}, defaultWhere, setting_options.toolbarObj.config.where);
        //属性字段
        //setting_options.url                //请求数据地址
        //setting_options.tableId            //table 的Id
        //setting_options.elem               //导出按钮ID   
        //setting_options.toolbarObj         //toolbar 对象     
        //setting_options.exportDataType     //导出数据的数据类型   xls、cvs   
        //setting_options.defaultWhere       //默认查询数据  数据格式json,如:{ pageIndex: 1, pageSize: 100000 }
        //setting_options.beforeBtnText      //导出按钮执行提示  
        //setting_options.afterBtnText       //导出按钮执行完成后提示
        //setting_options.isLayuiExport      //是否是layui自带导出方式  默认是    是：0  否：1   

        //事件回调
        //setting_options.done     //接管导出操作
        //setting_options.error    //错误信息
        //setting_options.beforeSend    //执行之前
        //setting_options.complete     //执行完成
        //setting_options.msg         //消息提示

        $.ajax({
            type: setting_options.ajaxType,
            dataType: 'json',
            url: setting_options.url,
            data: setting_options.exportWhere,
            beforeSend: function (res) {
                if (typeof (setting_options.beforeSend) != "undefined") {
                    setting_options.beforeSend(res);
                } else {
                    if (typeof (setting_options.beforeBtnText) != "undefined") {
                        if (typeof (setting_options.elem) != "undefined") {
                            document.getElementById(setting_options.elem).innerHTML = setting_options.beforeBtnText;
                        } else {
                            console.log("elem为空，按钮提示将不起作用");
                        }
                    }
                }
            },
            success: function (res) {
                if (typeof (setting_options.done) != "undefined") {
                    setting_options.done(res);
                } else {
                    if (res != null) {
                        if (res.Status == 200){
                            //创建导出表格
                            if ((!!window.ActiveXObject || "ActiveXObject" in window)) {
                                ExportData.CreateTable(res.Data.BusinessData, setting_options.toolbarObj);
                                ExportData.IeExport(res.Data.BusinessData, setting_options.toolbarObj);
                            } else {
                                if (setting_options.isLayuiExport == 0) {
                                    table.exportFile(setting_options.tableId, res.Data.BusinessData, setting_options.exportDataType);
                                } else {
                                    ExportData.CreateTable(res.Data.BusinessData, setting_options.toolbarObj);
                                    ExportData.ChromeExport();
                                }
                            }
                            if (typeof (setting_options.msg) != "undefined") {
                                setting_options.msg("导出成功");
                            }
                        } else {
                            setting_options.msg('请求错误：' + res.ErrorMessage + '，' + res.Status);
                        }
                    }
                }
            },
            complete: function (res) {
                if (typeof (setting_options.complete) != "undefined") {
                    setting_options.complete(res);
                } else {
                    if (typeof (setting_options.afterBtnText) != "undefined") {
                        if (typeof (setting_options.elem) != "undefined") {
                            document.getElementById(setting_options.elem).innerHTML = setting_options.afterBtnText;
                        } else {
                            console.log("elem为空，按钮提示将不起作用");
                        }

                    }
                }
            },
            error: function (res) {
                if (typeof (setting_options.error) != "undefined") {
                    setting_options.error("请求出错", res);
                }

            }
        });
    }
    ExportData.NextMonth = function (times) {
        var time = new Date(times);
        var year = time.getFullYear();
        var month = time.getMonth();
        var day = time.getDate();
        if (month == 11) {
            year = year + 1;
            month = 0;
        } else {
            month = month + 1;
        }
        return util.toDateString(new Date(year, month, day, time.getHours(), time.getMinutes(), time.getSeconds()), "yyyy-MM-dd HH:mm:ss");
    }
    ExportData.UpMonth = function (times) {
        var time = new Date(times);
        var year = time.getFullYear();
        var month = time.getMonth();
        var day = time.getDate();
        if (month == 0) {
            year = year - 1;
            month = 11;
        } else {
            month = month - 1;
        }
        return util.toDateString(new Date(year, month, day, time.getHours(), time.getMinutes(), time.getSeconds()), "yyyy-MM-dd HH:mm:ss");
    }
    ExportData.DiffMonth = function (time1, time2) {

        var date1 = new Date(time1);
        var date2 = new Date(time2);

        var year1 = date1.getFullYear();
        var month1 = date1.getMonth();
        var day1 = date1.getMonth();

        var year2 = date2.getFullYear();
        var month2 = date2.getMonth();
        var day2 = date1.getMonth();

        var dt1 = parseInt(year1 * 12 + month1);
        var dt2 = parseInt(year2 * 12 + month2);

        var m = Math.abs(dt1 - dt2);
        return m;
    }
    ExportData.GetMonthDay = function (years, months) {
        var d = new Date(years, months, 0);
        return d.getDate();
    }
    ExportData.DiffDay = function (date1, date2) {
        var time1 = Date.parse(date1);
        var time2 = Date.parse(date2);
        var nDays = Math.abs(parseInt((time2 - time1) / 1000 / 3600 / 24));
        return nDays;
    }
    ExportData.IsIE = function () {
        if ((!!window.ActiveXObject || "ActiveXObject" in window)) {
            return true;
        }
        return false;
    }
    ExportData.CreateTable = function (data, toolbarObj) {
        var titleLength = toolbarObj.config.cols[0].length;
        var colArry = toolbarObj.config.cols[0];
        var titleArry = [];    //头部行

        var titleHtml = '';
        var bodyHtml = '';

        for (var i = 0; i < titleLength; i++) {
            var ltype = colArry[i].type;
            var lhide = colArry[i].hide;
            var lfield = colArry[i].field;
            var ltitle = colArry[i].title;
            if (typeof (lfield) != "undefined") {
                if (lhide == false && ltype == "normal") {
                    titleHtml += "<td>" + ltitle + "</td>";
                    var tjson = { field: lfield, title: ltitle };
                    titleArry.push(tjson);
                }
            }
        }
        for (var i = 0; i < data.length; i++) {
            var trhtml = "";
            for (var j = 0; j < titleArry.length; j++) {
                var tfield = titleArry[j].field;
                var dfield = data[i][tfield];
                trhtml += "<td style='mso-number-format:\\@'>" + dfield + "</td>";
            }
            trhtml += "<tr>" + trhtml + "</tr>";
            bodyHtml += trhtml;
        }
        bodyHtml = "<tr>" + bodyHtml + "</tr>";
        var html = '<TABLE width="100 % " style="display:none;" id="tableExcel"><TBODY>' + titleHtml + bodyHtml + '</TBODY></TABLE>';
        $("#tableExcel").remove();
        $("body").append(html);
    }
    ExportData.IeExport = function (tableid, excelname) {
        tableid = "tableExcel";
        excelname = "table_" + new Date().getTime();
        var curTbl = document.getElementById("tableExcel");
        var oXL;
        try {
            oXL = new ActiveXObject("Excel.Application"); //创建AX对象excel
        } catch (e) {
            alert("无法启动Excel!\n\n如果您确信您的电脑中已经安装了Excel，" + "那么请调整IE的安全级别。\n\n具体操作：\n\n" + "工具 → Internet选项 → 安全 → 自定义级别 → 对没有标记为安全的ActiveX进行初始化和脚本运行 → 启用");
            return false;
        }
        var oWB = oXL.Workbooks.Add(); //获取workbook对象
        var oSheet = oWB.ActiveSheet;//激活当前sheet
        var sel = document.body.createTextRange();
        sel.moveToElementText(curTbl); //把表格中的内容移到TextRange中
        
        sel.select; //全选TextRange中内容
        sel.execCommand("Copy");//复制TextRange中内容
        oSheet.Paste();//粘贴到活动的EXCEL中
        oXL.Visible = true; //设置excel可见属性
        var fname = oXL.Application.GetSaveAsFilename("table_" + new Date().getTime()+ ".xls", "Excel Spreadsheets (*.xls), *.xls");
        oWB.SaveAs(fname);
        oWB.Close();
        oXL.Quit();
    }
    ExportData.ChromeExport = function (tableid, excelname) {
        tableid = "tableExcel";
        excelname = "table_" + new Date().getTime();

        var uri = 'data:application/vnd.ms-excel;base64,';
        var template = '<html><head><meta charset="UTF-8"></head><body><table>{table}</table></body></html>';
        if (!tableid.nodeType) table = document.getElementById(tableid)
        var ctx = { worksheet: excelname || 'Worksheet', table: table.innerHTML }
        var format = ExportData.Format(template, ctx);
        window.location.href = uri +window.btoa(unescape(encodeURIComponent(format)))
    }
    ExportData.Format = function (s, c) {
        return s.replace(/{(\w+)}/g,
            function (m, p) { return c[p]; })
    }
    exports('ExportData', ExportData);

});

