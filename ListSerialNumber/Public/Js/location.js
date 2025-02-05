const searchboxgrouplocation = document.querySelector('.search-box-group-location');
const searchboxbutton = document.querySelector('.search-box button');
const weatherbox = document.querySelector('.weather-box');
const weatherdetails = document.querySelector('.weather-details');
const errorMessage = document.querySelector('.error-message'); 
const cityInput = document.querySelector('.search-box input');
 
document.addEventListener('DOMContentLoaded', () => {
    cityInput.value = 'Ho Chi Minh';  
    fetchWeather('Ho Chi Minh'); 
});

searchboxbutton.addEventListener('click', () => {
    const city = cityInput.value; 
    fetchWeather(city); 
});

cityInput.addEventListener('keyup', (event) => {
    if (event.key === 'Enter') {
        const city = cityInput.value;  
        fetchWeather(city); 
    }
});
function fetchWeather(city) {
    const APIKey = '085895c59f36eb8536fbc7d50a9721d8';
    errorMessage.innerHTML = '';

    if (city === '') return;

    fetch(`https://api.openweathermap.org/data/2.5/weather?q=${city}&units=metric&appid=${APIKey}`)
        .then(response => {
            if (!response.ok) {
                throw new Error('Không tìm thấy thành phố');
            }
            return response.json();
        })
        .then(json => {
            console.log(json);

            const image = document.querySelector('.weather-box img');
            const temperature = document.querySelector('.weather-box .temperature');
            const description = document.querySelector('.weather-box .description');

            const humidity = document.querySelector('.weather-details .humidity span');
            const wind = document.querySelector('.weather-details .wind span');

            switch (json.weather[0].main) {
                case 'Clear':
                    image.src = 'Public/Img/sunny_2580627.png';
                    break;
                case 'Rain':
                    image.src = 'Public/Img/rain_8782155.png';
                    break;
                case 'Snow':
                    image.src = 'Public/Img/snowfall_1959398.png';
                    break;
                case 'Clouds':
                    image.src = 'Public/Img/cloudy_1710834.png';
                    break;
                case 'Mist':
                    image.src = 'Public/Img/windy_5034877.png';
                    break;
                case 'Haze':
                    image.src = 'Public/Img/storm_2864448.png';
                    break;
                default:
                    image.src = 'Public/Img/storm_2864448.png';
                    break;
            }

            temperature.innerHTML = `${parseInt(json.main.temp)}<span>&#176;C</span>`;
            description.innerHTML = `${json.weather[0].description}`;
            humidity.innerHTML = `${json.main.humidity} %`;
            wind.innerHTML = `${parseInt(json.wind.speed * 3.6)} km/h`;
        })
        .catch(error => {
            console.error('There was a problem with the fetch operation:', error);
            errorMessage.innerHTML = 'Thông báo: ' + error.message;
        });
}
