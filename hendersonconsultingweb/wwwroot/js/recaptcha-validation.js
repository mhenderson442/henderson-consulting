document.querySelector('form').addEventListener('submit', function (e) {
    let recaptchaResponse = document.getElementById('g-recaptcha-response').value;
    let recaptchaValidation = document.getElementById('g-recaptcha-validation');

    if (!recaptchaResponse) {
        e.preventDefault();
       
        if (recaptchaValidation) {
            recaptchaValidation.classList.remove("d-none");
        }
        else {
            alert('Please complete the reCAPTCHA before submitting the form.');
        }
    }
    else {
        if (recaptchaValidation) {
            recaptchaValidation.classList.add("d-none");
        }
    }
});