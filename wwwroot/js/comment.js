$('#contact-form').on("submit", function(e) {
    e.preventDefault();
     console.log(1)
    $.post('BlogComment', { __RequestVerificationToken: $('__RequestVerificationToken'), name: $('#name').val(), email: $('email').val(), subject: $('subject').val(), message: $('message').val() },
        function(returnedData) {
            console.log(returnedData);
        });

})