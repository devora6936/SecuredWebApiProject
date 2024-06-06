var arr =[];

const addToCart = (product) => {
    let basket = JSON.parse(sessionStorage.getItem("basket")) || [];
    const productIndex = basket.findIndex(p => p.product.productId === product.productId);

    if (productIndex === -1) {
        basket.push({ product: product, quantity: 1 });
    } else {
        basket[productIndex].quantity += 1;
    }

    sessionStorage.setItem("basket", JSON.stringify(basket));
    updateCartCount();
};

const updateCartCount = () => {
    let basket = JSON.parse(sessionStorage.getItem("basket")) || [];
    let totalItems = basket.reduce((sum, item) => sum + item.quantity, 0);
    document.getElementById("ItemsCountText").innerText = totalItems;
};


const drawProducts = (data) => {
    const productList = document.getElementById("temp-card");
    const productContainer = document.getElementById("PoductList");

    data.forEach(product => {
        const card = productList.content.cloneNode(true);

        card.querySelector('.card').setAttribute("id", product.productId);
        card.querySelector('h1').textContent = product.productName;
        card.querySelector('.price').textContent = product.price;
        card.querySelector('img').src = product.imageUrl;
        card.querySelector('button').addEventListener("click", () => addToCart(product));

        productContainer.appendChild(card);
    });
};


const importProducts = async (url = 'api/products') => {
    try {
        const response = await fetch(
            url, {
            method: 'GET',
            headers: { 'Content-Type': 'application/json' }
        }
        );

        if (!response.ok) {
            throw new Error('Failed to fetch products');
        }

        const data = await response.json();
        drawProducts(data);
        setPriceRange(data);

    } catch (error) {
        console.error('Error importing products:', error);
    }
};

const setPriceRange = (products) => {
    if (products.length === 0) return;

    let minPrice = Math.min(...products.map(product => product.price));
    let maxPrice = Math.max(...products.map(product => product.price));

    document.getElementById("minPrice").value = minPrice;
    document.getElementById("maxPrice").value = maxPrice;
};


window.addEventListener("load", () => {
    importProducts();
    updateCartCount();
});