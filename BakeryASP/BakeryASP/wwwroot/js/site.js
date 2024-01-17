// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let cartEl = document.getElementById("cart");

const addToOrder = (sandwich) => {
    const sandwichInput = document.querySelector(`input[id='${sandwich}']`);
    // const sandwichInput = document.querySelector(`input[id='${sandwich}']`);
    sandwichInput.value++
    const txtEl = sandwichInput.parentElement.querySelector("p");
    txtEl.innerText = `${sandwich}  x${sandwichInput.value}`;
}

const removeFromOrder = (sandwich) => {
    const sandwichInput = document.querySelector(`input[id='${sandwich}']`);
    const txtEl = sandwichInput.parentElement.querySelector("p");
    if (sandwichInput.value <= 1) {
        sandwichInput.value = 0;
        txtEl.innerText = "";
    } else {
        sandwichInput.value--;
        txtEl.innerText = `${sandwich}  x${sandwichInput.value}`;
    }
}