/*$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    console.log(result.results);
    let temp = "";
    $.each(result.results, (key, val) => {
        temp += `<tr>
                    <td>${key + 1}</td>
                    <td>${val.name}</td>
                    <td><button onclick="detail('${val.url}')" data-bs-toggle="modal" data-bs-target="#modalPokemon" class="btn btn-primary">Detail</button></td>
                </tr>`;
    })
    $("#tbodyPokemon").html(temp);
})*/



function detail(stringURL) {
    $.ajax({
        url: stringURL
    }).done(res => {
        let temp = "";
        $.each(res.types, (key, val) => {
            temp += `<div class="badge bg-primary">${val.type.name}</div> `;
        });

        $("#modalTitle").html(res.name);
        $("#pokemonHeight").text(res.height);
        $("#pokemonWeight").text(res.weight);
        $("#pokemonBaseExperience").text(res.base_experience);
        $("#pokemonAbilities").html(res.abilities.map(ability => `<span class="badge mr-2 text-center" style="background-color:${getAbilitiesColor(ability.ability.name)}; width: 100px; height: 20px; border-radius: 10px;">${ability.ability.name}</span>`).join(""));
        $("#pokemonStats").html(res.stats.map(stat => `
        <div>${stat.stat.name}</div>
        <div class="progress mb-2">
            <div class="progress-bar progress-bar-striped progress-bar-animated ${getStatColor(stat.base_stat)}" role="progressbar" style="width: ${stat.base_stat}%;" aria-valuenow="${stat.base_stat}" aria-valuemin="0" aria-valuemax="100">
                <div class="stat-label">${stat.base_stat}</div>
            </div>
        </div>`).join(""));
        $("#pokemonImage").attr("src", res.sprites.other.dream_world.front_default);
        $("#pokemonName").html(res.name);
        $("#pokemonTypes").html(res.types.map(type => `<span class="badge mr-2" style="background-color:${getTypeColor(type.type.name)}; width: 60px; height: 20px; border-radius: 10px;">${type.type.name}</span>`).join(""));
    });
}

function getStatColor(value) {
    if (value >= 80) {
        return "bg-success";
    } else if (value >= 50) {
        return "bg-warning";
    } else {
        return "bg-danger";
    }
}

function getTypeColor(value) {
    if (value == "fire") {
        return "#FF4500";
    } else if (value == "water") {
        return "#1E90FF";
    } else if (value == "grass") {
        return "#228B22";
    } else if (value == "poison") {
        return "#9233CC";
    } else if (value == "flying") {
        return "#87CEEB";
    } else if (value == "bug") {
        return "#9ACD32";
    } else if (value == "normal") {
        return "#A9A9A9";
    } else if (value == "electric") {
        return "#FFD700"; 
    } else {
        return "white";
    }
}

function getAbilitiesColor(value) {
    if ( value == "overgrow") {
        return "#006400"; // Hijau gelap
    } else if (value == "chlorophyll") {
        return "#32CD32"; // Hijau terang
    } else if (value == "blaze") {
        return "#FF4500"; // Merah terang
    } else if (value == "solar") {
        return "#FFD700"; // Kuning
    } else if (value == "torrent") {
        return "#1E90FF"; // Biru laut
    } else if (value == "rain-dish") {
        return "#87CEEB"; // Biru langit
    } else if (value == "shield-dust") {
        return "#C0C0C0"; // Abu-abu perak
    } else if (value == "run-away") {
        return "#8B4513"; // Cokelat
    } else if (value == "shed-skin") {
        return "#baba9e"; // Kulit
    } else if (value == "compound-eyes") {
        return "#4B0082"; // Ungu gelap
    } else if (value == "swarm") {
        return "#B8860B"; // Kuning gelap
    } else if (value == "sniper") {
        return "#000080"; // Biru tua
    } else if (value == "keen-eye") {
        return "#FFFF00"; // Kuning muda
    } else if (value == "tangled-feet") {
        return "#D3D3D3"; // Abu-abu muda
    } else if (value == "big-pecks") {
        return "#FFC0CB"; // Merah muda
    } else if (value == "guts") {
        return "#FF0000"; // Merah
    } else if (value == "hustle") {
        return "#696969"; // Abu-abu tua
    } else {
        return "#0000FF"; // Warna default jika tidak ada yang cocok
    }
}

$(document).ready(function () {
    $('#myTable').DataTable({
        ajax: {
            url: "https://pokeapi.co/api/v2/pokemon/?limit=50",
            dataType: "JSON",
            dataSrc: "results" //data source -> butuh array of object
        },
        columns: [
            {
                data: 'url',
                render: function (data, type, row) {
                    let number = data.split('/')[6]
                    // Mengembalikan nomor urut berdasarkan indeks
                    return number;
                }
            },
            { data: "name" },
            {
                data: null,
                render: function (data, type, row) {
                    return `<button onclick="detail('${data.url}')" data-bs-toggle="modal" data-bs-target="#modalPokemon" class="btn btn-primary">Detail</button>`;
                }
            },
        ]
    });
});



