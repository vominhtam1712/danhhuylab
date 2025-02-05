const spans = document.querySelectorAll('#falling-text span');

const wrapperMenu = document.getElementById('wrapperMenu');
const iconCloseMenu = document.getElementById('iconCloseMenu');
const iconOneMenu = document.getElementById('iconOneMenu');


const links = document.querySelectorAll('.navigation a');


links.forEach((link, index) => {
    link.addEventListener('click', function (e) {
        e.preventDefault(); 
         
        links.forEach(l => {
            l.classList.remove('loaded');
            l.classList.remove('reverse');
        });
         
        if (index < 3) {  
            setTimeout(() => {
                link.classList.add('loaded');
            }, index * 100); 
        } else { 
            link.classList.add('reverse');
            setTimeout(() => {
                link.classList.add('loaded');
            }, (links.length - index - 1) * 100); 
        }
         
        setTimeout(() => {
            window.location.href = link.href;
        }, 500);  
    });
});
 
iconCloseMenu.addEventListener('click', () => {
    wrapperMenu.classList.add('hidden');
});
 
iconOneMenu.addEventListener('click', () => {
    wrapperMenu.classList.remove('hidden');
});

function startFalling() {
    spans.forEach((span, index) => {
        setTimeout(() => {
            span.style.opacity = 1;  
            span.style.animation = `fall 2s forwards`; 
        }, index * 500);  
    });
}
 
startFalling();

