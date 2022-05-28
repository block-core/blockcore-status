// <![CDATA[
(function ($) {
    $.bootstrapModalAlert = function (options) {
        var defaults = {
            caption: 'Confirm operation',
            body: 'Will the requested operation be performed?'
        };
        options = $.extend(defaults, options);

        var alertContainer = "#alertContainer";
        var html = '<div class="modal fade" id="alertContainer">' 
            + '<div class="modal-dialog"><div class="modal-content"><div class="modal-header">'
            + '<h5>' + options.caption + '</h5>'
            + '<a class="close" data-dismiss="modal">&times;</a>'
            + '</div>'
            + '<div class="modal-body">'
            + options.body + '</div></div></div></div></div>';

        $(alertContainer).modal('hide');
        $('body').removeClass('modal-open');
        $('.modal-backdrop').remove();

        $(alertContainer).remove();
        $(html).appendTo('body');
        $(alertContainer).modal('show');
    };
})(jQuery);
// ]]>