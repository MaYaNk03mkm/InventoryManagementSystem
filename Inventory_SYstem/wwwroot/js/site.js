const username = document.getElementById("username");
const password = document.getElementById("password");
const inventoryBox = document.getElementById("inventoryBox");
const loginBtn = document.getElementById("loginBtn");
const loginForm = document.getElementById("loginForm");
const energyBeam = document.getElementById("energyBeam");

console.log(username);
console.log(password);
console.log(inventoryBox);
username.addEventListener("input", () => {
    for (let i = 0; i < 5; i++) {
        createParticle(username);
    }
    inventoryBox.style.transform = "scale(1.15)";

    setTimeout(() => {
        inventoryBox.style.transform = "scale(1)";
    }, 200);

});
password.addEventListener("input", () => {

    for (let i = 0; i < 5; i++) {
        createParticle(password);
    }

    inventoryBox.style.transform = "scale(1.15)";

    setTimeout(() => {
        inventoryBox.style.transform = "scale(1)";
    }, 200);

});
function createParticle(sourceElement) {
    const particle = document.createElement("div");

    particle.classList.add("particle");

    document.body.appendChild(particle);

    const start = sourceElement.getBoundingClientRect();    const end = inventoryBox.getBoundingClientRect();

    const randomX = (Math.random() - 0.5) * 80;
    const randomY = (Math.random() - 0.5) * 80;

    particle.style.left = (start.left + start.width / 2 + randomX) + "px";
    particle.style.top = (start.top + start.height / 2 + randomY) + "px";

    setTimeout(() => {

        particle.style.left = (end.left + end.width / 2) + "px";
        particle.style.top = (end.top + end.height / 2) + "px";

    }, 20);
    inventoryBox.classList.add("active");

    setTimeout(() => {
        inventoryBox.classList.remove("active");
    }, 250);

    setTimeout(() => {
        particle.remove();
    }, 900);

}
loginForm.addEventListener("submit", function (e) {

    e.preventDefault();

    loginBtn.disabled = true;
    loginBtn.innerHTML = "Authenticating";
    energyBeam.style.height = "120px";
    for (let i = 0; i < 10; i++) {
        createButtonParticle();
    }

    setTimeout(() => {
        energyBeam.style.height = "0";
        loginForm.submit();

    }, 1000);

});
/* ==========================
   Energy Beam
========================== */


function createButtonParticle() {

    const particle = document.createElement("div");

    particle.classList.add("particle");

    document.body.appendChild(particle);

    const start = inventoryBox.getBoundingClientRect();
    const end = loginBtn.getBoundingClientRect();

    particle.style.left = (start.left + start.width / 2) + "px";
    particle.style.top = (start.top + start.height / 2) + "px";

    setTimeout(() => {

        particle.style.left = (end.left + end.width / 2) + "px";
        particle.style.top = (end.top + end.height / 2) + "px";

    }, 20);

    setTimeout(() => {
        particle.remove();
    }, 900);

}