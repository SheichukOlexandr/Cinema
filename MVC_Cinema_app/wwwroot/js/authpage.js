document.addEventListener('DOMContentLoaded', function () {
    const container = document.querySelector('.container');
    const registerBtn = document.querySelector('.register-btn');
    const loginBtn = document.querySelector('.login-btn');

    if (registerBtn) {
        registerBtn.addEventListener('click', () => {
            container.classList.add('active');
        });
    }

    if (loginBtn) {
        loginBtn.addEventListener('click', () => {
            container.classList.remove('active');
        });
    }

    const togglePasswordVisibility = (toggleIds, passwordId, confirmPasswordId) => {
        const toggleElements = toggleIds.map(id => document.getElementById(id));
        const passwordElement = document.getElementById(passwordId);
        const confirmPasswordElement = document.getElementById(confirmPasswordId);

        if (toggleElements.every(el => el) && passwordElement && confirmPasswordElement) {
            toggleElements.forEach(toggleElement => {
                toggleElement.addEventListener('click', function () {
                    const type = passwordElement.getAttribute('type') === 'password' ? 'text' : 'password';
                    passwordElement.setAttribute('type', type);
                    confirmPasswordElement.setAttribute('type', type);

                    const placeholder = type === 'password' ? 'Пароль' : 'Видимий пароль';
                    passwordElement.setAttribute('placeholder', placeholder);
                    confirmPasswordElement.setAttribute('placeholder', placeholder);

                    toggleElements.forEach(el => {
                        el.classList.toggle('bxs-hide');
                        el.classList.toggle('bxs-show');
                    });
                });
            });
        }
    };

    togglePasswordVisibility(['toggleLoginPassword'], 'loginPassword', 'loginPassword');
    togglePasswordVisibility(['toggleRegisterPassword', 'toggleConfirmPassword'], 'registerPassword', 'confirmPassword');
});

