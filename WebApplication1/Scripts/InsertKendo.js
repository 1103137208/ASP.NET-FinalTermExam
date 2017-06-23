$(document).ready(function () {
    $.getScript("../Scripts/kendo.all.min.js", function () {
       
        $('#ContactTitle').kendoComboBox({
            dataTextField: "CodeVal",
            dataValueField: "CodeID",
            dataSource: {
                transport: {
                    read: "../Customer/GetContactTitle",
                    dataType: "json"
                }
            }
        })

        $("#ResetBtn").kendoButton();

        $("#InsertBtn").kendoButton({
            click: function () {
                var validator = $("#Form").data("kendoValidator");
                if (validator.validate()) {
                    $.ajax({
                        type: "POST",
                        url: "../Customer/DoInsertCustomer",
                        data: $("#Form").serialize(),
                        success: function (response) {
                            alert("Insert Success! CustomerID : " + response);
                            window.location.href = 'Index';
                        }
                    });
                }
            }
        })

        $("#CreationDate").kendoDatePicker({
            format: "yyyy/MM/dd",
            parseFormats: ["yyyy/MM/dd"]
        });


        var container = $("#Form");
        kendo.init(container);
        container.kendoValidator();
    })
})