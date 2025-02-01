const container = document.querySelector('.container');
const registerBtn = document.querySelector('.register-btn');
const loginBtn = document.querySelector('.login-btn');

registerBtn.addEventListener('click', () => {
    container.classList.add('active');
})

loginBtn.addEventListener('click', () => {
    container.classList.remove('active');
})

document.getElementById('toggleLoginPassword').addEventListener('click', function (e) {
    const password = document.getElementById('loginPassword');
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    this.classList.toggle('bxs-hide');
    this.classList.toggle('bxs-show');
});

document.getElementById('toggleRegisterPassword').addEventListener('click', function (e) {
    const password = document.getElementById('registerPassword');
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    this.classList.toggle('bxs-hide');
    this.classList.toggle('bxs-show');
});

document.getElementById('toggleConfirmPassword').addEventListener('click', function (e) {
    const password = document.getElementById('confirmPassword');
    const type = password.getAttribute('type') === 'password' ? 'text' : 'password';
    password.setAttribute('type', type);
    this.classList.toggle('bxs-hide');
    this.classList.toggle('bxs-show');
});
