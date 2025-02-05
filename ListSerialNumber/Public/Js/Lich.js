const monthYear = document.getElementById('monthYear');
const dates = document.getElementById('dates');
const prevButton = document.getElementById('prev');
const nextButton = document.getElementById('next');

let currentDate = new Date();
let lastTransform = ''; // Để lưu lớp chuyển động trước đó

function renderCalendar(animate) {
    const month = currentDate.getMonth();
    const year = currentDate.getFullYear();

    monthYear.textContent = `${year} - ${month + 1}`;

    if (animate) {
        monthYear.classList.add(lastTransform);
        setTimeout(() => {
            monthYear.classList.remove(lastTransform);
        }, 500); // Thời gian cho hiệu ứng
    }

    dates.innerHTML = '';
    const firstDay = new Date(year, month, 1).getDay();
    const lastDate = new Date(year, month + 1, 0).getDate();

    const today = new Date();
    const todayDate = today.getDate();
    const todayMonth = today.getMonth();
    const todayYear = today.getFullYear();

    for (let i = 0; i < firstDay; i++) {
        const emptyDiv = document.createElement('div');
        emptyDiv.classList.add('empty');
        dates.appendChild(emptyDiv);
    }

    for (let date = 1; date <= lastDate; date++) {
        const dateDiv = document.createElement('div');
        dateDiv.textContent = date;

        if (date === todayDate && month === todayMonth && year === todayYear) {
            dateDiv.classList.add('today');
        }

        dates.appendChild(dateDiv);
    }
}

prevButton.addEventListener('click', () => {
    lastTransform = 'slide-right'; // Hiệu ứng trái
    currentDate.setMonth(currentDate.getMonth() - 1);
    renderCalendar(true);
});

nextButton.addEventListener('click', () => {
    lastTransform = 'slide-left'; // Hiệu ứng phải
    currentDate.setMonth(currentDate.getMonth() + 1);
    renderCalendar(true);
});

// Render calendar lần đầu
renderCalendar(false);
