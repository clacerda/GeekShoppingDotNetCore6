// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function applyMask(input) {
    let value = input.value.replace(/\D/g, '');
    if (value.length >= 2) {
        input.value = value.slice(0, 2) + '/' + value.slice(2, 4);
    } else {
        input.value = value;
    }
}

function validateExpiryDate(input) {
    const errorSpan = document.getElementById('expiryError');
    const regex = /^(0[1-9]|1[0-2])\/?([0-9]{2})$/;

    if (!regex.test(input.value)) {
        errorSpan.innerText = "Invalid format. Use MM/YY.";
        return;
    }

    const today = new Date();
    const month = parseInt(input.value.substring(0, 2), 10);
    const year = parseInt("20" + input.value.substring(3, 5), 10);

    const currentYear = today.getFullYear();
    const currentMonth = today.getMonth() + 1;

    if (year > currentYear + 5) {
        errorSpan.innerText = "The expiration date it's wrong.";
        return;
    }

    if (year < currentYear || (year === currentYear && month < currentMonth)) {
        errorSpan.innerText = "The expiration date is in the past.";
        return;
    }

    errorSpan.innerText = "";
}
function getCreditCardIcons(cardNumber) {

    const sanitizedCardNumber = cardNumber.replace(/\s|-/g, '');

    const visaRegex = /^4[0-9]{12}(?:[0-9]{3})?(?:[0-9]{3})?$/;
    const mastercardRegex = /^(5[1-5][0-9]{14}|2[2-7][0-9]{14})$/;
    const amexRegex = /^3[47][0-9]{13}$/;
    const discoverRegex = /^6(?:011|5[0-9]{2}|4[4-9][0-9]|22[1-9][0-9]{2})[0-9]{12}$/;
    const dinersClubRegex = /^3(?:0[0-5]|[68][0-9])[0-9]{11}$/;
    const jcbRegex = /^35(?:2[89]|[3-8][0-9])[0-9]{12}$/;

    if (visaRegex.test(sanitizedCardNumber)) return "input-group-text fab fa-cc-visa fa-2x";
    if (mastercardRegex.test(sanitizedCardNumber)) return "input-group-text fab fa-cc-mastercard fa-2x";
    if (amexRegex.test(sanitizedCardNumber)) return "input-group-text fab fa-cc-amex fa-2x";
    if (discoverRegex.test(sanitizedCardNumber)) return "input-group-text fab fa-cc-discover fa-2x";
    if (dinersClubRegex.test(sanitizedCardNumber)) return "input-group-text fab fa-cc-diners-club fa-2x";
    if (jcbRegex.test(sanitizedCardNumber)) return "input-group-text fab fa-cc-jcb fa-2x";

    return "input-group-text fa fa-credit-card fa-2x";
}

function updateCardIcon() {
    const cardNumber = document.getElementById('cardNumber').value;
    const icon = getCreditCardIcons(cardNumber);
    document.getElementById('cardIcon').className = icon;
}