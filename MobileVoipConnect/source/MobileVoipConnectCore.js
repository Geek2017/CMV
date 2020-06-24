"use strict";
var _createClass = (function () {
    function i(e, t) {
        for (var n = 0; n < t.length; n++) {
            var i = t[n];
            (i.enumerable = i.enumerable || !1), (i.configurable = !0), "value" in i && (i.writable = !0), Object.defineProperty(e, i.key, i);
        }
    }
    return function (e, t, n) {
        return t && i(e.prototype, t), n && i(e, n), e;
    };
})();
function _classCallCheck(e, t) {
    if (!(e instanceof t)) throw new TypeError("Cannot call a class as a function");
}
var MobileVoipConnectCore = (function () {
        function t() {
            var e = this;
            _classCallCheck(this, t),
                (this.name = "mobile-voip-connect "),
                (this.rewriting = !1),
                (this.coldNds = ["button", "code", "input", "img", "select", "script", "submit", "style", "textarea"]),
                (this.telNumRegExs = [
                    /(1-)?(1 )?[0-9]{3}\-[0-9]{3}\-[0-9]{4}?/g,
                    /[0-9]{3}\.[0-9]{3}\.[0-9]{4}?/g,
                    /[0-9]{3}[ ][0-9]{3}[ ][0-9]{4}?/g,
                    /\([0-9]{3}\)[ ][0-9]{3}\-[0-9]{4}?/g,
                    /\([0-9]{3}\)\-[0-9]{3}\-[0-9]{4}?/g,
                    /\+[0-9]{2}[ ][0-9]{3}[ ][0-9]{3}\-[0-9]{4}?/g,
                    /\+[0-9]{2}[ ]\([0-9]{3}\)[ ][0-9]{3}\-[0-9]{4}?/g,
                ]),
                (this.observer = new MutationObserver(function () {
                    e.dmUpdate(e);
                })),
                (this.config = { subtree: !0, attributes: !1, childList: !0, characterData: !0 });
        }
        return (
            _createClass(t, [
                { key: "init", value: function () {} },
                {
                    key: "startRewrite",
                    value: function (e) {
                        this.rewriting || ((this.rewriting = !0), this.clearNds(e), this.parseNd(e), (this.rewriting = !1), this.observer.observe(document.body, this.config));
                    },
                },
                {
                    key: "dmUpdate",
                    value: function (e) {
                        e.observer.disconnect();
                        setTimeout(function () {
                            e.startRewrite(document.body), e.observer.observe(document.body, e.config);
                        }, 10);
                    },
                },
                {
                    key: "getNdType",
                    value: function (e) {
                        var t = e.type;
                        if (void 0 !== t && "" !== t) {
                            for (; void 0 !== t.type; ) t = t.type;
                            t = t.toString().toLowerCase();
                        }
                        return t;
                    },
                },
                {
                    key: "getNdValue",
                    value: function (e) {
                        var t = null;
                        return e.val ? (t = e.val) : e.value ? (t = e.value) : e.textContent && (t = e.textContent), t;
                    },
                },
                {
                    key: "getNdValue",
                    value: function (e) {
                        var t = null;
                        return e.val ? (t = e.val) : e.value ? (t = e.value) : e.textContent && (t = e.textContent), t;
                    },
                },
                {
                    key: "getLineHeight",
                    value: function (e) {
                        return $(e).parent().css("font-size");
                    },
                },
                {
                    key: "clearNds",
                    value: function (e) {
                        $(e).find(".MobileVoipConnect TelNum span").remove(),
                            $(e)
                                .find(".MobileVoipConnect TelNum")
                                .each(function () {
                                    $(this).replaceWith(this.innerHTML);
                                });
                    },
                },
                {
                    key: "parseNd",
                    value: function (e) {
                        if (void 0 === e) return !1;
                        var t = e.nodeName.toLowerCase(),
                            n = (this.getNdType(e), e.childNodes.length),
                            i = "";
                        if (-1 < $.inArray(t, this.coldNds)) return !1;
                        if ("span" === t && $(e).hasClass("MobileVoipConnect TelNum")) return !1;
                        for (var r = 0; r < n; r++) this.parseNd(e.childNodes[r]);
                        return e.nodeType === Node.TEXT_NODE && null !== e.parentElement.isContentEditable && !1 === e.parentElement.isContentEditable && ((i = this.getLineHeight(e)), this.parsePhoneNumbers(e, i)), !1;
                    },
                },
                {
                    key: "parsePhoneNumbers",
                    value: function (e, t) {
                        var n = String(this.getNdValue(e)),
                            i = [];
                        if (null === n || 0 === n.trim().length || 0 === n.replace(/[\D]*/, "").length) return null;
                        for (var r in this.telNumRegExs) {
                            var o = n.match(this.telNumRegExs[r]);
                            o &&
                                o.forEach(function (e) {
                                    i.push(e);
                                });
                        }
                        i.length && this.updateNd(i, e, t);
                    },
                },
                {
                    key: "regularizedNumber",
                    value: function (e) {
                        var t = e;
                        return (t = t.replace(/[^0-9\+\,]/g, "")), t;
                    },
                },
                {
                    key: "updateNd",
                    value: function (e, t, r) {
                        var o = this,
                            a = String($(t).parent().html());
                        (e = e.filter(function (e, t, n) {
                            return n.indexOf(e) === t;
                        })).forEach(function (e) {
                            var t = e.replace(/[\-\[\]\/\{\}\(\)\*\+\?\.\\\^\$\|]/g, "\\$&"),
                                n = new RegExp(t, "g"),
                                i = o.createNewNd(e, o.regularizedNumber(e), r);
                            a = (a = a.replace(n, i)).replace("document.write", "//document.write");
                        }),
                            e.length && $(t).parent().html(a);
                    },
                },
                {
                    key: "createNewNd",
                    value: function (e, t, n) {
                        return (
                            '<span class="MobileVoipConnect TelNum" style="display:inline-block;"><span title="Dial ' +
                            e +
                            '" style="white-space: nowrap; display: inline-block; cursor: pointer; padding-right: 2px; vertical-align: text-bottom; height: ' +
                            n +
                            ';" onclick="window.open(\'https://connectmevoice.mobilevoipconnect.com/dial?n=' +
                            t +
                            '\'); return(false);"><svg style="display: inline; height: 100%; width: auto; margin: 0;" version="1.1" id="Shape_1_1_" xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" x="0px" y="0px" viewBox="0 0 12 12" style="enable-background:new 0 0 12 12;" xml:space="preserve"><style type="text/css">.st0{fill-rule:evenodd;clip-rule:evenodd;fill:#E01C32;}</style><g><path class="st0" d="M11.1,8.2C9.5,6.6,8.6,7.5,8,8.2L7.9,8.3C7.5,8.6,6.5,8,5.3,6.8C3.2,4.7,3.6,4.2,3.7,4.1L3.8,4c0.7-0.7,1.5-1.5,0-3.1C3.3,0.4,2.9,0,2.4,0C1.8,0,1.3,0.5,0.9,0.9c0.1-0.1-1.5,1.2-0.5,3.7C0.9,6,1.7,7.4,3.2,8.9c1.4,1.4,2.8,2.2,4.3,2.8c2.4,1,3.7-0.6,3.7-0.5c0.4-0.4,1-0.9,0.9-1.6C12,9.1,11.6,8.7,11.1,8.2z"/></g></svg></span>' +
                            e +
                            "</span>"
                        );
                    },
                },
            ]),
            t
        );
    })(),
    cmvc = new MobileVoipConnectCore();
chrome.extension.onMessage.addListener(function (e, t, n) {
    void 0 !== e && cmvc.startRewrite(document.body);
}),
    chrome.extension.sendRequest({ pageLoad: !0 }, function (e) {
        cmvc.startRewrite(document.body);
    });
