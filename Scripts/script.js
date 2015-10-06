var table;
var Owner = {};
var Paper = {};
var Finish = {};
var Printer = {};
var Customer = {};
var Purchase = {};
var XHC = {
    paperValue: new Array(),
    finishnum: 1,
    finishingItems: new Array(),
    optionFinishItem: "",
    mapUnit: new Array(),
    total: 0,
    qty: 0,
    paper_cost: 0,
    finish_cost: 0,
    printer_cost: 0,
};

Login = function (data)
{
    if (data.error == null) {
        window.location = GetSiteRoot() + data.url;
    } else {
        $("#display-error").html(data.error);
    }
}

function GetSiteRoot() {
    var rootPath = window.location.protocol + "//" + window.location.host;
    if (window.location.hostname == "localhost") {
        var path = window.location.pathname;
        if (path.indexOf("/") == 0) {
            path = path.substring(1);
        }
        path = path.split("/", 1);
        if (path != "") {
            rootPath = rootPath;
        }
    }
    return rootPath;
}

LoadingStart = function () {
    $('#loading').modal('show');
}

LoadingEnd = function (type) {
    $('#loading').modal('hide');
    switch (type) {
        case "success":
            $('#save-success').find('.save-txt').text('บันทึกข้อมูลเรียบร้อย');
            $('#save-result').find('.modal-body').html($('#save-success').html());
            $('#save-result').modal('show');
            setTimeout(function () { $('#save-result').modal('hide') }, 3000);
            break;
    }
}


/////////////// Start Owner ////////////

Owner.edit = function (id) {
    $.ajax({
        url: GetSiteRoot() + "/Owner/Edit",
        type: "GET",
        cache: false,
        data: { 'id': id },
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("แก้ไขข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Owner.editSuccess = function () {
    $('#form-modal').modal('hide');
    table.fnReloadAjax();
}

Owner.Register = function () {
    $.ajax({
        url: GetSiteRoot() + "/Owner/Register",
        type: "GET",
        cache: false,
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("เพิ่มผู้ใช้ระบบ");
            $('#form-modal').modal('show');
        }
    });
}

Owner.registerSuccess = function () {   
    $('#form-modal').modal('hide');
    table.fnReloadAjax();
}

Owner.remove = function (id) {
    if (confirm("คุณต้องการลบผู้ใช้นี้ ?") == true) {
        $.ajax({
            url: GetSiteRoot() + "/Owner/Remove",
            type: "POST",
            data: {id : id},
            cache: false,
            success: function (response) {
                if (response.result == true) {
                    table.fnReloadAjax();
                }
            }
        });
    }
}


///////////// End Owner //////////////////


///////////// Start Paper ///////////////

Paper.edit = function (id) {
    $.ajax({
        url: GetSiteRoot() + "/StockPaper/Edit",
        type: "GET",
        cache: false,
        data: { 'id': id },
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("แก้ไขข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Paper.editSuccess = function () {
    $('#form-modal').modal('hide');
    table.fnReloadAjax();       
}

Paper.Register = function () {
    $.ajax({
        url: GetSiteRoot() + "/StockPaper/Register",
        type: "GET",
        cache: false,
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("เพิ่มข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Paper.registerSuccess = function () {
    $('#form-modal').modal('hide');
    table.fnReloadAjax();
}

Paper.remove = function (id) {
    if (confirm("คุณต้องการลบรายการนี้ ?") == true) {
        $.ajax({
            url: GetSiteRoot() + "/StockPaper/Remove",
            type: "POST",
            data: { id: id },
            cache: false,
            success: function (response) {
                if (response.result == true) {
                    table.fnReloadAjax();
                }
            }
        });
    }
}

///////////// End Paper //////////////




///////////// Start Finishing ///////////////

Finish.edit = function (id) {
    $.ajax({
        url: GetSiteRoot() + "/Finishing/Edit",
        type: "GET",
        cache: false,
        data: { 'id': id },
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("แก้ไขข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Finish.editSuccess = function () {
    $('#form-modal').modal('hide'); console.log(table);
    table.fnReloadAjax();
}

Finish.Register = function () {
    $.ajax({
        url: GetSiteRoot() + "/Finishing/Register",
        type: "GET",
        cache: false,
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("เพิ่มข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Finish.registerSuccess = function () {
    $('#form-modal').modal('hide');
    table.fnReloadAjax();
}

Finish.remove = function (id) {
    if (confirm("คุณต้องการลบรายการนี้ ?") == true) {
        $.ajax({
            url: GetSiteRoot() + "/Finishing/Remove",
            type: "POST",
            data: { id: id },
            cache: false,
            success: function (response) {
                if (response.result == true) {
                    table.fnReloadAjax();
                }
            }
        });
    }
}

///////////// End Finishing //////////////

//////////// Start Printer ///////////////

Printer.edit = function (id) {
    $.ajax({
        url: GetSiteRoot() + "/Printer/Edit",
        type: "GET",
        cache: false,
        data: { 'id': id },
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("แก้ไขข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Printer.editSuccess = function () {
    $('#form-modal').modal('hide'); 
    table.ajax.reload();
}

Printer.Register = function () {
    $.ajax({
        url: GetSiteRoot() + "/Printer/Register",
        type: "GET",
        cache: false,
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("เพิ่มข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Printer.registerSuccess = function () {
    $('#form-modal').modal('hide');
    table.ajax.reload();
}

Printer.remove = function (id) {
    if (confirm("คุณต้องการลบรายการนี้ ?") == true) {
        $.ajax({
            url: GetSiteRoot() + "/Printer/Remove",
            type: "POST",
            data: { id: id },
            cache: false,
            success: function (response) {
                if (response.result == true) {
                    table.ajax.reload();
                }
            }
        });
    }
}

////////// End Printer ///////////////

////////// Start Customer ///////////////

Customer.edit = function (id) {
    $.ajax({
        url: GetSiteRoot() + "/Customer/Edit",
        type: "GET",
        cache: false,
        data: { 'id': id },
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("แก้ไขข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Customer.editSuccess = function () {
    $('#form-modal').modal('hide');
    table.ajax.reload();
}

Customer.Register = function () {
    $.ajax({
        url: GetSiteRoot() + "/Customer/Register",
        type: "GET",
        cache: false,
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("เพิ่มข้อมูล");
            $('#form-modal').modal('show');
        }
    });
}

Customer.registerSuccess = function () {
    $('#form-modal').modal('hide');
    table.ajax.reload();
}

Customer.remove = function (id) {
    if (confirm("คุณต้องการลบรายการนี้ ?") == true) {
        $.ajax({
            url: GetSiteRoot() + "/Home/Remove",
            type: "POST",
            data: { id: id },
            cache: false,
            success: function (response) {
                if (response.result == true) {
                    table.ajax.reload();
                }
            }
        });
    }
}

////////// End Customer ///////////////

Purchase.remove = function (id) {
    if (confirm("คุณต้องการลบรายการนี้ ?") == true) {
        $.ajax({
            url: GetSiteRoot() + "/Home/Remove",
            type: "POST",
            data: { id: id },
            cache: false,
            success: function (response) {
                if (response.result == true) {
                    table.fnReloadAjax();
                }
            }
        });
    }
}

XHC.OpenHint = function (type) {
    var url = null;
    switch (type.toLowerCase()) {
        case 'job':
            url = GetSiteRoot() + "/Search/JobDesc";
            break;
        case 'paper':
            url = GetSiteRoot() + "/Search/PaperDesc";
            break;
        case 'printer':
            url = GetSiteRoot() + "/Search/PrinterDesc";
            break;
        case 'finish':
            url = GetSiteRoot() + "/Search/FinishDesc";
            break;
        case 'transfer':
            url = GetSiteRoot() + "/Search/TransferDesc";
            break;
        case 'customer':
            url = GetSiteRoot() + "/Search/CustomerDesc";
            break;
    } 
    $.ajax({
        url: url,
        type: "GET",
        cache: false,
        data: {},
        success: function (response) {
            $('#form-modal').find('.modal-body').html(response);
            $('#form-modal').find('.modal-title').html("Detail");
            $('#form-modal').modal('show');
        }
    });
}

XHC.onload = function () {
    var id = $('#paper').val();
    var start = (XHC.paperValue[id][0] == null) ? 0 : XHC.paperValue[id][0];
    var end = (XHC.paperValue[id][1] == null) ? 0 : XHC.paperValue[id][1];
    $('#sum_size').val(start + 'X' + end);
    if (start > 0 || end > 0) {
        $('#select-sum-size').removeClass('col-md-9').addClass('col-md-7');
        if ($('#select-sum-size').next().attr('id') == 'show_sum_size') {
            $('#show_sum_size').remove();
        }
        var str = '<label class="col-md-2 control-label label-left" id="show_sum_size">' + start + ' X ' + end + '</label>';
        $(str).insertAfter('#select-sum-size');
    } else {
        $('#select-sum-size').removeClass('col-md-7').addClass('col-md-9');
        if ($('#select-sum-size').next().attr('id') == 'show_sum_size') {
            $('#show_sum_size').remove();
        }
    }
}

XHC.selectSize = function (obj) {
    var id = $(obj).val();
    var start = (XHC.paperValue[id][0] == null) ? 0 : XHC.paperValue[id][0];
    var end = (XHC.paperValue[id][1] == null) ? 0 : XHC.paperValue[id][1];
    $('#sum_size').val(start + 'X' + end);
    if (start > 0 || end > 0) {
        $('#select-sum-size').removeClass('col-md-9').addClass('col-md-7');
        if ($('#select-sum-size').next().attr('id') == 'show_sum_size') {
            $('#show_sum_size').remove();
        }
        var str = '<label class="col-md-2 control-label label-left" id="show_sum_size">' + start + ' X ' + end + '</label>';
        $(str).insertAfter('#select-sum-size');
    } else {
        $('#select-sum-size').removeClass('col-md-7').addClass('col-md-9');
        if ($('#select-sum-size').next().attr('id') == 'show_sum_size') {
            $('#show_sum_size').remove();
        }
    }
    //$('#gsm').val(XHC.paperValue[id][2]);
}

XHC.selectFinish = function (id, obj) {
    var val = $(obj).val();
    $('#finish_type_' + id).html(XHC.mapUnit[val]);
}

XHC.CreateFinishingElm = function () {
    if (XHC.finishnum == 7) return false;
    if (XHC.optionFinishItem == "") {
        for (var x in XHC.finishingItems) {
            XHC.optionFinishItem += '<option value="' + XHC.finishingItems[x].Value + '">' + XHC.finishingItems[x].Text + '</option>';
        }
    }
    XHC.finishnum++;
    var str = '<div id="finish_block_xxnoxx">';
    str += '<div class="col-md-10" id="finish_block_xxnoxx">';
    str += '<form class="form-horizontal" role="form">';
    str += '<div class="form-group">';
    str += '<label class="col-md-3 control-label"></label>';
    str += '<label class="col-md-1 control-label">xxnoxx</label>';
    str += '<div class="col-md-6">';
    str += '<select id="finishing_xxnoxx" class="selectpicker form-control" data_live_search=true onchange="XHC.selectFinish(xxnoxx, this);">';
    str += XHC.optionFinishItem;
    str += '</select>';
    str += '</div>';
    str += '<div class="col-md-2 control-label" id="finish_type_xxnoxx">' + XHC.mapUnit[Object.keys(XHC.mapUnit).shift()] + '</div>';
    str += '</div>';
    str += '</form>';
    str += '</div>';
    str += '<div class="col-md-2">';
    str += '<div class="col-md-1"><button type="button" class="btn btn-info bnt-margi-top" onclick="XHC.RemoveFinishingEle(xxnoxx)"><i class="glyphicon glyphicon-minus"></i></button></div>';
    str += '</div>';
    str += '</div>';
    str = str.split('xxnoxx').join(XHC.finishnum);
    $('#finis_block').append(str);
    $('#finishing_' + XHC.finishnum).selectpicker();
    
}

XHC.RemoveFinishingEle = function (id) {
    var tmp = new Array();
    var j = 0;
    var str = ''
    for (var i = 2; i <= XHC.finishnum; ++i) {
        tmp[j] = '';
        if ($('#finishing_' + i).val() != '') {
            tmp[j] = $('#finishing_' + i).val();
        }
        ++j;
        $('#finish_block_' + i).remove();
    }
    var j = 0;
    var y = 2;
    for (var i = 2; i <= XHC.finishnum; ++i) {
        if (i != id) {
            var str = '<div id="finish_block_xxnoxx">';
            str += '<div class="col-md-10">';
            str += '<form class="form-horizontal" role="form">';
            str += '<div class="form-group">';
            str += '<label class="col-md-3 control-label"></label>';
            str += '<label class="col-md-1 control-label">xxnoxx</label>';
            str += '<div class="col-md-6">';
            str += '<select id="finishing_xxnoxx" class="selectpicker form-control" data_live_search=true onchange="XHC.selectFinish(xxnoxx, this);">';
            str += XHC.optionFinishItem;
            str += '</select>';
            str += '</div>';
            str += '<div class="col-md-2 control-label" id="finish_type_xxnoxx">' + XHC.mapUnit[Object.keys(XHC.mapUnit).shift()] + '</div>';
            str += '</div>';
            str += '</form>';
            str += '</div>';
            str += '<div class="col-md-2">';
            str += '<div class="col-md-1"><button type="button" class="btn btn-info bnt-margi-top" onclick="XHC.RemoveFinishingEle(xxnoxx)"><i class="glyphicon glyphicon-minus"></i></button></div>';
            str += '</div>';
            str += '</div>';
            str = str.split('xxnoxx').join(y);
            $('#finis_block').append(str);
            $('#finishing_' + y).selectpicker();
            if (tmp[j] != null) {
                tmp[j].split('##');
                $('#finishing_' + y).val(tmp[j]).change();
            }
            ++y;
        }
        ++j;
     }
     XHC.finishnum--;
}

XHC.Calculate = function (type) {
    var qty = $('#qty').val();
    var lose = $('#lose').val();
    var paper = $('#paper').val();
    var sum_size = $('#sum_size').val();
    var width = $('#width').val();
    var height = $('#height').val();
    var printer = $('#printer').val();
    var qty_color = $('#qty_color').val();
    var qty_black = $('#qty_black').val();
    var customer = $('#customer').val();
    var transfer = $('#transfer').val();
    var datetime = $('#recieve_date').val();
    
    var arr = new Array();
    var j = 0;
    var str_f = '';
    var str = '';
    for (var i = 1; i <= XHC.finishnum; ++i) {
        arr[j] = $('#finishing_' + i).val();
        if($('#finishing_' + i).val() < 1){
            str_f += '<li>กรุณาระบุหลังงานพิมพ์ที่ ' + i + '</li>';
        }
        j++;
    }
    if (qty < 1 || qty == '') {
        str += '<li>กรุณากรอกจำนวนกระดาษ </li>';
    }
    if (paper < 1 || paper == '') {
        str += '<li>กรุณาเลือกประเภทกระดาษ </li>';
    }
    if (width < 1 || width == '') {
        str += '<li>กรุณาระบุความกว้าง </li>';
    }
    if (height < 1 || height == '') {
        str += '<li>กรุณาระบุความยาว </li>';
    }
    if (printer < 1 || printer == '') {
        str += '<li>กรุณาระบุเครื่องพิมพ์ </li>';
    }
    var profit = 30;
    var vat = 7;
    if (type == 'save') {
        if ($('#customer').val() == null) {
            str += '<li>กรุณาระบุชื่อลูกค้า</li>';
        }
        profit = ($('#input-sell').val() == '') ? 0 : $('#input-sell').val();
        vat = ($('#input-vat').val() == '') ? 0 : $('#input-vat').val();
    }
    str += str_f;
    if (str != '') {
        str = '<ul style="font-size:0.9em;">'+str+'</ul>';
        $('#save-warning').find('.save-txt').html(str);
        $('#save-result').find('.modal-body').html($('#save-warning').html());
        $('#save-result').find('.modal-footer').css({'margin-top':'20px'});
        $('#save-result').modal('show');
        return false;
    }
    $.ajax({
        url: GetSiteRoot() + "/Search/Calculate",
        type: "POST",
        cache: false,
        beforeSend: function(){ $('#loading').modal('show'); },
        data: { qty: qty, lose: lose, paper: paper, sum_size: sum_size, width: width, height: height, finishing: arr, printer: printer, qty_black: qty_black, qty_color: qty_color, customer: customer, transfer: transfer, datetime: datetime, profit: profit, vat: vat },
        success: function (rs) {            
            var str = $('#search-result').html();
            str = str.split('xxcost_paperxx').join(rs.cost_paper);
            str = str.split('xxcost_printerxx').join(rs.cost_printer);
            str = str.split('xxcost_finishxx').join(rs.cost_finish);
            str = str.split('xxtotalxx').join(rs.total);
            str = str.split('xxsellxx').join(rs.sell);
            str = str.split('xxunitxx').join(rs.unit);
            str = str.split('xxvatxx').join(rs.vat);
            str = str.split('xxvat_unitxx').join(rs.vat_unit);
            str = str.split('xxvat-inputxx').join(7);
            str = str.split('xxprofitxx').join(30);
            $('#show-result').html(str);
            XHC.finish_cost = parseFloat(rs.cost_finish.replace(',', ''));
            XHC.printer_cost = parseFloat(rs.cost_printer.replace(',', ''));
            XHC.paper_cost = parseFloat(rs.cost_paper.replace(',', ''));
            XHC.total = parseFloat(rs.total.replace(',', ''));
            XHC.qty = parseFloat(rs.qty.replace(',', ''));
            $('#loading').modal('hide');
        }
    });
}

XHC.CalculateSell = function (obj){
    var val = $(obj).val();
    var vat = $('#input-vat').val();
    vat = parseFloat(vat.replace(',', ''));
    var total = XHC.total;
    var sell = (total * val / 100) + total;
    var unit = sell / parseFloat(XHC.qty);
    var vat_price = (sell * vat / 100) + sell;
    var vat_price_unit = vat_price / XHC.qty;
    $('#sell-price').html(numberWithCommas(sell));
    $('#sell-price-unit').html(numberWithCommas(unit));
    $('#vat-price').html(numberWithCommas(vat_price));
    $('#vat-price-unit').html(numberWithCommas(vat_price_unit));
}

XHC.CalculateVat = function (obj) {
    var val = $(obj).val();
    var input_sell = $('#input-sell').val();
    var total = XHC.total;
    var sell = (total * input_sell / 100) + total;
    var vat_price = (sell * val / 100) + sell;
    var vat_price_unit = vat_price / XHC.qty;
    $('#vat-price').html(numberWithCommas(vat_price));
    $('#vat-price-unit').html(numberWithCommas(vat_price_unit));
}

XHC.SaveCalculate = function () {
    XHC.Calculate('save');
    var qty = $('#qty').val();
    var lose = $('#lose').val();
    var paper = $('#paper').val();
    var sum_size = $('#sum_size').val();
    var width = $('#width').val();
    var height = $('#height').val();
    var printer = $('#printer').val();
    var qty_color = $('#qty_color').val();
    var qty_black = $('#qty_black').val();
    var customer = $('#customer').val();
    var transfer = $('#transfer').val();
    var recieve_date = ConvertDateTimeToDB($('#recieve_date').val());
    var profit = $('#input-sell').val();
    var vat = $('#input-vat').val();
    var job_type = $('#job_type').val(); 
    var arr = new Array();
    var j = 0;
    for (var i = 1; i <= XHC.finishnum; ++i) {
        arr[j] = $('#finishing_' + i).val();
        j++;
    }

    $.ajax({
        url: GetSiteRoot() + "/Search/SaveSearch",
        type: "POST",
        cache: false,
        beforeSend: function () { $('#loading').modal('show'); },
        data: { qty: qty, lose: lose, paper: paper, sum_size: sum_size, width: width, height: height, finishing: arr, printer: printer, qty_black: qty_black, qty_color: qty_color, customer: customer, transfer: transfer, recieve_date: recieve_date, profit: profit, vat: vat, total: XHC.total, job_type: job_type, paper_cost: XHC.paper_cost, finish_cost: XHC.finish_cost, printer_cost: XHC.printer_cost },
        success: function (rs) {
            $('#loading').modal('hide');
            alert('บันทึกข้อมูลเรียบร้อย');
        }
    });
}

function ConvertDateTimeToDB(str) {
    str = str.split('/');
    return str[2] + '-' + str[1] + '-' + str[0];
}
function CheckNumber(obj) {
    var val = $(obj).val();
    str = '';
    for (var i = 0; i < val.length; ++i) {
        if (!isNaN(val[i])) {
            str += val[i];
        }
    }
    $(obj).val(str);
}

function numberWithCommas(x) {
    x = x.toFixed(2);
    return x.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}