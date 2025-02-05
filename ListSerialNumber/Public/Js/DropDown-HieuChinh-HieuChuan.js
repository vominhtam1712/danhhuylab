function toggleCardBody(index) {
    var cardBody = document.getElementById(`card-body-${index}`);
    var toggleIcon = document.getElementById(`toggle-icon-${index}`);
    var isVisible = cardBody.classList.contains('hidden');

    if (isVisible) {
        cardBody.classList.remove('hidden');
        toggleIcon.setAttribute('name', 'arrow-up-outline');
        localStorage.setItem(`cardBodyVisible${index}`, 'true');
    } else {
        cardBody.classList.add('hidden');
        toggleIcon.setAttribute('name', 'arrow-down-outline');
        localStorage.setItem(`cardBodyVisible${index}`, 'false');
    }
}
function loadCardBodyState() {
    for (let i = 1; i <= 7; i++) {
        var cardBody = document.getElementById(`card-body-${i}`);
        var toggleIcon = document.getElementById(`toggle-icon-${i}`);
        var isVisible = localStorage.getItem(`cardBodyVisible${i}`);

        if (isVisible === 'true') {
            cardBody.classList.remove('hidden');
            toggleIcon.setAttribute('name', 'arrow-up-outline');
        } else {
            cardBody.classList.add('hidden');
            toggleIcon.setAttribute('name', 'arrow-down-outline');
        }
    }
}
window.onload = loadCardBodyState;