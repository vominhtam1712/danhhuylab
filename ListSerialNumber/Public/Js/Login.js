const wrapper = document.querySelector('.wrapper');
const btnLoginPopup = document.querySelector('.btnLogin-popup');
const iconClose = document.querySelector('.icon-close');


const wrapperMenu = document.querySelector('.wrapper-menu');
const iconCloseMenu = document.querySelector('.icon-close-menu');
const iconOneMenu = document.querySelector('.icon-one-menu');


setTimeout(() => {
    wrapper.classList.add('active-popup');  
}, 500);  
 
btnLoginPopup.addEventListener('click', () => {
    wrapper.classList.add('active-popup');  
    wrapper.classList.remove('hidden'); 
});
 
iconClose.addEventListener('click', () => { 
    wrapper.classList.add('hidden');
    wrapper.classList.remove('active-popup');  
});

window.addEventListener('load', () => {
    wrapperMenu.classList.add('active');
});


iconCloseMenu.addEventListener('click', () => {
    wrapperMenu.classList.remove('active');
});

iconOneMenu.addEventListener('click', () => {
    wrapperMenu.classList.add('active');
});


