var XpathWorker = (function () {
    function XpathWorker() {
    }
    XpathWorker.createXPathFromElement = function (elm) {
        var allNodes = document.getElementsByTagName('*');
        for (var segs = []; elm && elm.nodeType == 1; elm = elm.parentNode) {
            if (elm.hasAttribute('id')) {
                var uniqueIdCount = 0;
                for (var n = 0; n < allNodes.length; n++) {
                    if (allNodes[n].hasAttribute('id') && allNodes[n].id == elm.id)
                        uniqueIdCount++;
                    if (uniqueIdCount > 1)
                        break;
                }
                ;
                if (uniqueIdCount == 1) {
                    segs.unshift('//*[@id="' + elm.getAttribute('id') + '"]');
                    return segs.join('/');
                }
                else {
                    segs.unshift(elm.localName.toLowerCase() + '[@id="' + elm.getAttribute('id') + '"]');
                }
            }
            else {
                for (var i = 1, sib = elm.previousSibling; sib; sib = sib.previousSibling) {
                    if (sib.localName == elm.localName)
                        i++;
                }
                ;
                segs.unshift(elm.localName.toLowerCase() + '[' + i + ']');
            }
            ;
        }
        ;
        return segs.length ? '/' + segs.join('/') : null;
    };
    ;
    XpathWorker.lookupElementByXPath = function (path) {
        var evaluator = new XPathEvaluator();
        var result = evaluator.evaluate(path, document.documentElement, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null);
        return result.singleNodeValue;
    };
    XpathWorker.hoverElementByEvent = function (event) {
        var target = XpathWorker.createXPathFromElement(event.target || event.srcElement);
        var element = XpathWorker.lookupElementByXPath(target);
        if (XpathWorker.LastElement) {
            XpathWorker.LastElement.classList.remove("XpathWorker_hovered");
            element.classList.add("XpathWorker_hovered");
            XpathWorker.LastElement = element;
        }
        else {
            element.classList.add("XpathWorker_hovered");
            XpathWorker.LastElement = element;
        }
    };
    XpathWorker.selectElementByEvent = function (event) {
        var target = XpathWorker.createXPathFromElement(event.target || event.srcElement);
        var element = XpathWorker.lookupElementByXPath(target);
        XpathWorker.SendXPath(target, element);
        if (XpathWorker.LastSelectedElement) {
            XpathWorker.LastSelectedElement.classList.remove("XpathWorker_selected");
            XpathWorker.LastSelectedElement.classList.remove("XpathWorker_active");
            element.classList.add("XpathWorker_active");
            XpathWorker.LastSelectedElement = element;
        }
        else {
            element.classList.add("XpathWorker_active");
            XpathWorker.LastSelectedElement = element;
        }
    };
    XpathWorker.selectElementByXPath = function (xpath) {
        var element = XpathWorker.lookupElementByXPath(xpath);
        element.classList.add("XpathWorker_selected");
    };
    XpathWorker.SetEctiveElement = function (xpath) {
        if (xpath.length > 0) {
            var element = XpathWorker.lookupElementByXPath(xpath);
            if (element != null) {
                if (XpathWorker.LastSelectedElement) {
                    XpathWorker.LastSelectedElement.classList.remove("XpathWorker_active");
                    XpathWorker.LastSelectedElement.classList.add("XpathWorker_selected");
                    element.classList.add("XpathWorker_active");
                    XpathWorker.LastSelectedElement = element;
                }
                else {
                    element.classList.add("XpathWorker_active");
                    XpathWorker.LastSelectedElement = element;
                }
            }
        }
    };
    XpathWorker.SendXPath = function (XPath, element) {
        var parent = window.parent;
        var ElementValue = element.textContent;
        var sendObject = {
            XPath: XPath,
            value: ElementValue
        };
        parent.postMessage(sendObject, '*');
    };
    return XpathWorker;
}());
XpathWorker.Active = false;
XpathWorker.LastElement = null;
XpathWorker.LastSelectedElement = null;
window.addEventListener("mouseover", function (event) {
    if (XpathWorker.Active) {
        var xPathWorker = XpathWorker.hoverElementByEvent(event);
    }
});
window.addEventListener("click", function (event) {
    event.preventDefault();
    event.stopPropagation();
    if (XpathWorker.Active) {
        var xPathWorker = XpathWorker.selectElementByEvent(event);
    }
});
var onMessage = function (e) {
    var data = e.data;
    var origin = e.origin;
    /**
     * Проверка октуда пришел запрос
     */
    console.log(origin);
    if (origin !== "http://localhost:3000") {
        console.log('Запрос пришел с другого домена');
        return;
    }
    var str = 'Пришли неверные данные';
    if (data.title && data.value) {
        str = 'Сообщение:' + data.title + '. Значение объекта:' + data.value;
    }
    switch (data.title) {
        case 'Active Input':
            XpathWorker.SetEctiveElement(data.value);
            XpathWorker.Active = true;
            break;
        case 'Config Iframe':
            for (var i = 0; i < data.value.Fields.length; i++) {
                XpathWorker.selectElementByXPath(data.value.Fields[i].XPath);
            }
            break;
    }
};
if (typeof window.addEventListener != 'undefined') {
    window.addEventListener('message', onMessage, false);
}
//# sourceMappingURL=XpathWorker.js.map