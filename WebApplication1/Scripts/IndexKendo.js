$(document).ready(function () {
    $.getScript("../Scripts/kendo.all.min.js", function () {
        $("#Grid").kendoGrid({
            dataSource: {
                transport: {
                    read: {
                        url: "../Customer/Read",
                        dataType: "json",
                    },
                },
                pageSize: 20
            },
            columns: [{
                field: "CustomerID",
                title: "編號",
                width: "50px"
            }, {
                field: "CompanyName",
                title: "名稱",
                width: "100px"
            }, {
                field: "ContactName",
                title: "聯絡人姓名",
                width: "100px"
            }, {
                field: "ContactTitle",
                title: "聯絡人職稱",
                width: "100px"
            }, {
                command: [{
                    name: "Update",
                    click: UpdateCustomer
                }, {
                    name: "Delete",
                    click: DeleteCustomer
                }], title: "&nbsp;", width: "100px"
            }],
            pageable: {
                pageSizes: true,
                buttonCount: 5
            },
            editable: false,
            sortable: true,
        });

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


        $("#SearchBtn").kendoButton({
            click: function () {
                $.ajax({
                    type: "POST",
                    url: "../Customer/Read",
                    data: $("#Form").serialize(),
                    success: function (response) {
                        var dataSource = new kendo.data.DataSource({
                            data: response,
                            pageSize: 20
                        });
                        var grid = $("#Grid").data("kendoGrid");
                        grid.setDataSource(dataSource);
                    }
                });
            }

        });

        //按鈕設置
        $("#ResetBtn").kendoButton();
        $("#InsertBtn").kendoButton({
            click: function () {
                console.log("click");
                window.location.href = 'InsertCustomer';
            }
        });


        function DeleteCustomer(e) {
            var tr = e.currentTarget.closest('tr');
            var dataItem = this.dataItem(tr);
            console.log(dataItem.CustomerID);
            $.ajax({
                type: "POST",
                url: "/Customer/DeleteCustomer",
                data: {
                    "CustomerID": dataItem.CustomerID
                },
                dataType: "json",
                success: function (response) {
                    if (response) {
                        alert(dataItem.CustomerID + " 刪除成功")
                        var grid = $("#Grid").data("kendoGrid");
                        grid.dataSource.remove(dataItem);
                        tr.remove();
                    } else {

                        alert( dataItem.CustomerID + " 刪除失敗")
                    }
                }
            });
        }

        function UpdateCustomer(e) {
            var tr = e.currentTarget.closest('tr');
            var dataItem = this.dataItem(tr);
            var CustomerID = dataItem.CustomerID;
            window.location.href = 'UpdateCustomer?CustomerID=' + CustomerID;
        }
    })

})

