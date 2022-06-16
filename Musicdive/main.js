const api = 'https://localhost:44333';

const template = document.getElementById('concert');
const input = document.getElementById('text-input');
const button = document.getElementById('submit');
const cities = document.getElementById('cities');
const concertList = document.getElementById('concert-list');

window.addEventListener('load', e => {
    loadCities();
    loadConcerts();
})

async function showConcerts(c) {
    const listItem = template.content.firstElementChild.cloneNode(true);
    const concert = listItem.querySelector('.name');
    const artist = listItem.querySelector('.artist');
    const date = listItem.querySelector('.date');
    const city = listItem.querySelector('.city');

    let concertDate = new Date(c.date).toLocaleDateString('sv-SE');
    let artistString = "";
    for (let artist of c.artists) {
        artistString += artist.name + " / ";
    };

    concert.innerText = c.name;
    artist.innerText = artistString;
    date.innerText = concertDate;
    city.innerText = c.city.name;

    concertList.append(listItem);
};

async function loadConcerts() {
    let url = api + '/api/musicdive';
    const response = await fetch(url);
    const concerts = await response.json();

    concertList.replaceChildren();
    for (const concert of concerts) {
        showConcerts(concert)
    }
};

async function loadCities() {
    let url = api + '/api/city';

    const response = await fetch(url);
    const citiesList = await response.json();

    for (const city of citiesList) {
        cities.add(new Option(city.name))
    }
};

async function loadConcertsByArtist() {
    let url = api + '/api/concert/artist?artist=' + input.value + '&city=' + cities.value;

    const response = await fetch(url);
    const concerts = await response.json();

    const concertCount = Object.values(concerts)
    if (!concertCount.length) {
        input.value = "";
        input.setAttribute('placeholder', 'No concert was found!');
    } else {
        input.setAttribute('placeholder', 'Artist');
    }

    concertList.replaceChildren();
    for (const concert of concerts) {
        showConcerts(concert)
    }
}

async function loadConcertByCity() {
    let url = api + '/api/concert/city?city=' + cities.value;

    const response = await fetch(url);
    const concerts = await response.json();

    const concertCount = Object.values(concerts)
    if (!concertCount.length) {
        input.value = "";
        input.setAttribute('placeholder', 'No concert was found!');
    } else {
        input.setAttribute('placeholder', 'Artist');
    }

    concertList.replaceChildren();
    for (const concert of concerts) {
        showConcerts(concert)
    }
}

button.addEventListener("click", e => {
    e.preventDefault();
    if (input.value) {
        loadConcertsByArtist();
    } else {
        loadConcertByCity();
    }
});