var select = document.querySelector('#selectedProvinces');
var selectDistrict = document.querySelector('#selectedDistrict');
var selectedWarn = document.querySelector('#selectedWarn');
const BASE_URL = 'http://dev.s-erp.com.vn:9038/v1/countries/VN/provinces';

function getProvinces() {
    fetch(`${BASE_URL}?page=1&size=66`)
        .then(response => response.json())
        .then(data => {
            data.data.content.forEach(country => {
                select.options[select.options.length] = new Option(country.name, country.id);
            });
        })
        .catch(error => console.error(error));
}

function getDistrict(id) {
    while (selectDistrict.options.length > 1) {
        selectDistrict.remove(1);
    }
    fetch(`${BASE_URL}/${id}/districts`)
        .then(response => response.json())
        .then(data => {
            data.data.content.forEach(country => {
                const option = document.createElement('option');
                option.value = country.id;
                option.textContent = country.name;
                selectDistrict.add(option);
            });
        })
        .catch(error => console.error(error));
}

function getWard() {
    this.getCountry(select.value, selectDistrict.value);
}

function getCountry(idProvinces, idDistrict) {
    if (!idProvinces && !idDistrict) {
        return;
    }
    while (selectedWarn.options.length > 1) {
        selectedWarn.remove(1);
    }

    fetch(`${BASE_URL}/${idProvinces}/districts/${idDistrict}/wards`)
        .then(response => response.json())
        .then(data => {
            data.data.content.forEach(country => {
                const option = document.createElement('option');
                option.value = country.id;
                option.textContent = country.name;
                selectedWarn.add(option);
            });
        })
        .catch(error => console.error(error));
}

function main() {
    getProvinces();
}

function handle() {
    var resultText = document.querySelector(".text-result");

    resultText.innerHTML = " Tỉnh " + select.options[select.selectedIndex].text + " Huyện " +
        selectDistrict.options[selectDistrict.selectedIndex].text + " Phường " +
        selectedWarn.options[selectedWarn.selectedIndex].text
}

main();