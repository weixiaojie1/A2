var ascending = true;
function sortData(direction) {
    var products = Array.from(document.querySelectorAll("#jian a"));

    products.sort(function (a, b) {
        var priceA = Number(a.getAttribute("data-price"));
        var priceB = Number(b.getAttribute("data-price"));

        if (direction === 'ascending') {
            return priceA - priceB;
        } else {
            return priceB - priceA;
        }
    });

    var jianElement = document.getElementById("jian");
    jianElement.innerHTML = "";
    for (var i = 0; i < products.length; i++) {
        jianElement.appendChild(products[i]);
    }
    ascending = direction === 'ascending';
}