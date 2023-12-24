// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

let cartEl;
document.onreadystatechange = () => {
    cartEl = document.getElementById("cart");
}
const addToOrder = (sandwich) => {
    console.log(sandwich)
    const sandwichInput = document.querySelector(`input[name='${sandwich}']`);
    sandwichInput.value++
    const txtEl = sandwichInput.parentElement.querySelector("p");
    txtEl.innerText = `${sandwich}  x${sandwichInput.value}`;
}