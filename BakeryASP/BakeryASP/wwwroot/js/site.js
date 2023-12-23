// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let orderEl;
document.onreadystatechange = () => {
    orderEl = document.getElementById("order");
}
const addToOrder = (sandwich) => {
    console.log(sandwich)
    const sandwichOrderCount = document.querySelector(`input[name='${sandwich}']`)
    sandwichOrderCount.value++;
    const orderTxt = `${sandwich} x${sandwichOrderCount.value}`;
    const orderItem = document.querySelector(`#order [id='${sandwich}']`)
    console.log(orderItem)
    if (orderItem != null) {
        orderItem.innerText = orderTxt
    } else {
        const html = `
            <div id="${sandwich}">
                ${orderTxt}
            </div>
        `
        orderEl.innerHTML += html;
    }
}