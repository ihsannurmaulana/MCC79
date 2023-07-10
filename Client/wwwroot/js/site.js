let column1 = document.getElementById("file1");
let column2 = document.getElementById("file2");
let column3 = document.getElementById("file3");

let button1 = document.getElementById("btn-1");
let button2 = document.getElementById("btn-2");
let button3 = document.getElementById("btn-3");
let button4 = document.getElementById("btn-4");

let defaultText = column1.innerHTML;

let reset = () => {
    column1.innerHTML = defaultText;
    column1.style.backgroundColor = "";
    column2.style.backgroundColor = "";
    column3.style.backgroundColor = "";
}

button1.addEventListener('click', () => {
    reset();
    column1.innerHTML = "Berubah dong!!!"
    column1.style.backgroundColor = "yellow";
})

button2.addEventListener('click', () => {
    reset();
    column2.style.backgroundColor = "blue";
})

button3.addEventListener('click', () => {
    reset();
    column3.style.backgroundColor = "red";
})

button4.addEventListener('click', () => {
    reset();
})



let arrayMhsObj = [
    { nama: "budi", nim: "a112015", umur: 20, isActive: true, fakultas: { name: "komputer" } },
    { nama: "joko", nim: "a112035", umur: 22, isActive: false, fakultas: { name: "ekonomi" } },
    { nama: "herul", nim: "a112020", umur: 21, isActive: true, fakultas: { name: "komputer" } },
    { nama: "herul", nim: "a112032", umur: 25, isActive: true, fakultas: { name: "ekonomi" } },
    { nama: "herul", nim: "a112040", umur: 21, isActive: true, fakultas: { name: "komputer" } },
];

let fakultasKomputer = [];

for (let i = 0; i < arrayMhsObj.length; i++) {
    let mahasiswa = arrayMhsObj[i];

    // Soal 1 
    // untuk mencari fakultas dengan name "komputer"
    if (mahasiswa.fakultas.name === "komputer") {
        fakultasKomputer.push(mahasiswa);
    }

    // Soal 2 
    // untuk mencari nim dengan 2 angka terakhir lebih dari 30
    let nimLast = parseInt(mahasiswa.nim.slice(-2));
    if (nimLast >= 30) {
        mahasiswa.isActive = false;
    }
}

console.log(fakultasKomputer);
console.log(arrayMhsObj);
