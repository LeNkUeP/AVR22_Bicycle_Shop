// Funktionen, die für alle Seiten zur Verfügung stehen.
// Registrierung erfolgt in index.html
window.JsFunctions =
{
    SubmitForm: function (form) { form.submit(); },
    Alert: function (text) { alert(text); },
    SetValue: function (element, newValue) { element.value = newValue; },
    SetContent: function (element, content) { element.innerHTML = content; },
    GetIFrameInnerHtml: function () { return frames[0].document.body.innerHTML; }
};