function silver_table(options) {
    var settings = $.extend({
        table_class: "",
        table_id: "silver-table",
        row_class: "",
        json: null,
        cells: null,
        show_row_count: false,
        enable_filter: false,
        filter: null
    }, options);

    var current_ctr = 0;
    var no_row = $("<tr class='no-row'>");
    no_row.append("<td colspan='" + (settings.cells.length + (settings.show_row_count ? 1 : 0)) + "'>No records found</td>");

    var table = $("<table id='" + settings.table_id + "' class='" + settings.table_class + "'>");

    var thead = $("<thead>");
    thead.append("<tr></tr>");
    if (settings.show_row_count == true) {
        thead.find("tr").append("<th>#</th>");
    }  
    for (c in settings.cells) {
        thead.find("tr").append("<th>" + settings.cells[c]["label"] + "</th>");
    }
    table.append(thead);

    var tbody = $("<tbody>");
    for (j in settings.json) {
        current_ctr++;
        var row = $("<tr class='" + settings.row_class + "'>");
        if (settings.show_row_count == true) {
            row.append("<td>" + current_ctr + "</td>");
        }
        for (c in settings.cells) {
            var cell = $("<td class='" + settings.cells[c].class + "'>");
            for (a in settings.cells[c].attributes) {
                var temp_data = settings.json[j][settings.cells[c].attributes[a].value];
                if (settings.cells[c].data_type == "date") {
                    temp_data = convert_to_js_date(temp_data);
                }
                cell.attr(settings.cells[c].attributes[a].label, temp_data);
            }
            if (settings.cells[c].is_single_line == true) {
                var temp_data = settings.json[j][settings.cells[c].data];
                switch (settings.cells[c].data_type) {
                    case "date":
                        temp_data = convert_to_js_date(temp_data);
                        break;
                    case "number":
                        temp_data = FormatMoney(temp_data.toFixed(2));
                        break;
                    case "custom":
                        temp_data = settings.cells[c].custom_value;
                        break;
                    default:
                        break;
                }
                cell.html(temp_data);
            } else { 
                //todo
            }
            row.append(cell);
        }
        row.data(settings.json[j]);
        tbody.append(row);
    }
    if (current_ctr == 0) {
        tbody.append(no_row);
    }
    table.append(tbody);

    if (settings.enable_filter == true) {
        $("#" + settings.filter.ul_filter_id + " a").live("click", function () {
            var _this = $(this);
            if (current_ctr > 0) {
                $(".no-row").remove();
                if (_this.attr("rel") == "ALL") {
                    $("." + settings.row_class).removeClass("hide");
                } else {
                    $("." + settings.row_class).addClass("hide");
                    if ($("." + settings.row_class).find("." + settings.filter.cell + "[rel='" + _this.attr("rel") + "']").length == 0) {
                        tbody.append(no_row);
                    } else {
                        $("." + settings.row_class).find("." + settings.filter.cell + "[rel='" + _this.attr("rel") + "']").closest("tr").removeClass("hide");
                    }
                }
            }
            $("#" + settings.filter.ul_filter_id + " li").removeClass("active");
            _this.closest("li").addClass("active");
        });
    }

    return table;
}

//format date
function convert_to_js_date(date) {
    var pattern = /Date\(([^)]+)\)/;
    var results = pattern.exec(date);
    var dt = new Date(parseFloat(results[1]));
    return dt.getFullYear() + "-" + (dt.getMonth() + 1) + "-" + dt.getDate();
}