
$('#search-form').on("submit", function (e) {
    e.preventDefault();
    
    let key = $('#search-input').val();
    let url = window.location.href.slice(23)

        $.ajax({
            url: url+'/Search?key=' + key,
            type: 'Get',
            success: function (res) {
                $(`#${url}list`).html(res);
            }
        })
    
   
})

//console.log(window.location.href.slice(23))

