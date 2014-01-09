
function CallHandler() {
    $.ajax({
        url: "Handler/MyHandler.ashx",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: { 'Id': '10000' },
        responseType: "json",
        success: OnComplete,
        error: OnFail
    });
    return false;
}
function GetInput() {
    var emp = new Object();
    emp.Name = 'Brij';
    emp.Age = '27';
    return emp;
}
function OnComplete(result) {
    alert([result.Id, result.Name, result.Age, result.Department]);
}
function OnFail(result) {
    alert('Ajax failed');

}