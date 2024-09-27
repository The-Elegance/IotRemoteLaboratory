let monacoInterop = {};
monacoInterop.editors = {};

monacoInterop.initialize = function initialize(elementId, initialCode, language, dotnetHelper) {
    require.config({ paths: { 'vs': 'monaco-editor/min/vs', 'esm-vs': 'monaco-editor/esm/vs/' } });

    require(['vs/editor/editor.main'], function initializeEditor() {

        var editor = monaco.editor.create(document.getElementById(elementId), {
            value: initialCode,
            domReadOnly: true,
            language: language,
            automaticLayout: true,
            theme: "vs-dark",
        });

        editor.onDidChangeModelContent(() => {
            dotnetHelper.invokeMethodAsync('OnCodeChanged', monacoInterop.editors[elementId].getValue());
        });

        monacoInterop.editors[elementId] = editor;
    });
}


monacoInterop.getCode = function getCode(elementId) {
    return monacoInterop.editors[elementId].getValue();
}
monacoInterop.setCode = function setCode(elementId, code) {
    monacoInterop.editors[elementId].setValue(code);
}

monacoInterop.reload = function reload(elementId) {
    monacoInterop.editors[elementId] = monacoInterop.editors[elementId];
}

monacoInterop.readonlyMode = function readonlyMode(elementId, state) {
    var position = {
        lineNumber: 1,
        column: 1,
    };

    monacoInterop.editors[elementId].updateOptions({ readOnly: state });
}

monacoInterop.changeFontSize = function changeFontSize(elementId, fontSize) {
    monacoInterop.editors[elementId].updateOptions({ fontSize: fontSize });
}

window.monacoInterop = monacoInterop;