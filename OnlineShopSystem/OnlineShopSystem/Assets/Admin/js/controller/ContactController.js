var content = {
    init: function () {
        content.registerEvents();
    },
    registerEvents: function () {
        $('.btn-active').off('click').on('click', function (e) {
            e.preventDefault();
            var btn = $(this);
            var id = btn.data('id');
            $.ajax({
                url: "/Admin/Contact/ChangeStatus",
                data: { id: id },
                dataType: "json",
                type: "POST",
                success: function (respone) {
                    console.log(respone);
                    if (respone.status == true) {
                        btn.text("Kich hoat");
                    } else {
                        btn.text("Khoa");
                    }
                }
            });
        });
    }
}
content.init();