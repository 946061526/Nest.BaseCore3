layui.define("form", function (exports) {
    var MOD_NAME = "interact",
        o = layui.jquery,
        form = layui.form,
        elems = [],
        hints = [],
        datas = [],
        events = [],
        interact = function () { };
    interact.prototype.on = function (event, callback) {
        events.push(event);
        return layui.onevent.call(this, MOD_NAME, event, callback)
    };
    interact.prototype.render = function (e) {
        datas.push(this.data(e.data));
        elems.push(e.elem);
        hints.push(e.hint ? e.hint : "");
        this.template(e)
    };
    interact.prototype.template = function (e) {
        var t = this,
            arr = ['<label class="layui-form-label">' + e.title + "</label>"],
            hint = e.hint ? e.hint : ["请选择"];
        if (e.selected) {
            t.selected(e, arr, hint)
        } else {
            o.each(hint, function (idx, value) {
                var options = (idx == 0 ? t.options(e, 0).join("") : "");
                arr.push('<div class="layui-input-inline"><select name="' + e.name + '[]"><option value="">' + value + "</option>" + options + "</select></div>")
            })
        }
        o(e.elem).html(arr.join(""));
        t.refresh(e)
    };
    interact.prototype.select = function (e, obj, value) {
        var t = this,
            index = t.curr_idx(obj),
            otl = obj.parents(".layui-input-inline"),
            ot = otl.next();
        if (value !== "" && (!datas[t.curr_idx(obj)][value] || !datas[index][value].length)) {
            otl.nextAll().remove();
            return false
        }
        ot.length && otl.nextAll().find("select option:not(option:first)").remove();
        if (value !== "") {
            var i = otl.index();
            if (!ot || !ot.length) {
                ot = otl.after('<div class="layui-input-inline"><select name="' + e.name + '[]"><option value="">' + (hints[index] && hints[index][i] ? hints[index][i] : "请选择") + "</option></select></div>").next()
            }
            var options = [ot.find("option:first").prop("outerHTML")];
            ot.find("select").html(t.options(e, value, options, obj).join(""))
        }
        t.refresh(e)
    };
    interact.prototype.selected = function (e, arr, hint) {
        var t = this;
        var objArr = []
        for (let key in t.data(e.data)) {
            objArr.push(key)
        }
        if (objArr[0] != 0) {
            e.selected.unshift(objArr[0]);
        } else {
            e.selected.unshift(0);
        }

        o.each(e.selected, function (idx, value) {
            if (idx < e.selected.length - 1) {
                var options = "";
                o.each(t.data(e.data)[value], function (index, item) {
                    options += '<option value="' + item.id + '" ' + (item.id == e.selected[idx + 1] ? "selected" : "") + ">" + item.title + "</option>"
                });
                arr.push('<div class="layui-input-inline"><select name="' + e.name + '[]"><option value="">' + (hint[idx] ? hint[idx] : "请选择") + "</option>" + options + "</select></div>")
            }
        });
        return arr
    };
    interact.prototype.options = function (e, value, arr, obj) {
        var t = this;
        arr = arr ? arr : [];
        var objArr = []
        for (let key in datas[t.curr_idx(obj)]) {
            objArr.push(key)
        }
        //console.log(datas[t.curr_idx(obj)][value])
        if (!datas[t.curr_idx(obj)][value]) {
            console.log("1=" + objArr[0]);
            o.each(datas[t.curr_idx(obj)][objArr[0]], function (idx, item) {
                arr.push('<option value="' + item.id + '">' + item.title + "</option>")
            });
        } else {
            o.each(datas[t.curr_idx(obj)][value], function (idx, item) {
                arr.push('<option value="' + item.id + '">' + item.title + "</option>")
            });
        }
        return arr
    };
    interact.prototype.refresh = function (e) {
        var t = this;
        form.render();
        o(elems.join(",")).find(".layui-anim dd").click(function () {
            var obj = o(this),
                value = obj.attr("lay-value"),
                filter = obj.parents("[lay-filter]");
            t.select(e, obj, value);
            return filter ? layui.event.call(this, MOD_NAME, "interact(" + filter.attr("lay-filter") + ")", {
                elem: obj.parents(".layui-input-inline").find("select option[value=" + value + "]"),
                othis: obj,
                value: value,
                text: obj.text()
            }) : ""
        })
    };
    interact.prototype.curr_idx = function (obj) {
        var idx = 0;
        if (obj) {
            var cls = obj.parents(".layui-form-item").attr("class").replace("layui-form-item ", "");
            idx = elems.findIndex(function (val) {
                return val == "." + cls
            })
        }
        return idx
    };
    interact.prototype.data = function (data) {
        var arr = [];
        o.each(data, function (index, item) {
            if (!arr[item.pid]) {
                arr[item.pid] = []
            }
            arr[item.pid].push(item)
        });
        return arr
    };
    exports(MOD_NAME, new interact())
});