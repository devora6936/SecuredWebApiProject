var arr = [];

var categories = [];


const handleChange = (event) => {
    const categoryId = event.target.id;

    if (event.currentTarget.checked) {
        addCategory(categoryId);
    } else {
        removeCategory(categoryId);
    }

    filterProducts();
}

const addCategory = (categoryId) => {
    if (!categories.includes(categoryId)) {
        categories.push(categoryId);
    }
}

const removeCategory = (categoryId) => {
    categories = categories.filter(item => item !== categoryId);
}

const filterProducts = () => {
    const url = buildProductFilterUrl();
    console.log(url);

    clearProductList();
    importProducts(url);
}

const buildProductFilterUrl = () => {
    const minPrice = document.getElementById('minPrice').value;
    const maxPrice = document.getElementById('maxPrice').value;
    const desc = document.getElementById('nameSearch').value;

    const categoriesStr = categories.map(id => `&categoryIds=${id}`).join('');
    return `api/products?minPrice=${minPrice}&maxPrice=${maxPrice}${categoriesStr}&desc=${desc}`;
}

const clearProductList = () => {
    document.getElementById("PoductList").replaceChildren();
}

const drawCategories = (data) => {
    console.log(data);
    const categoriesTemplate = document.getElementById("temp-category");

    data.forEach(category => {
        const card = categoriesTemplate.content.cloneNode(true);
        setupCategoryCard(card, category);
        document.getElementById("categoryList").appendChild(card);
    });
}

const setupCategoryCard = (card, category) => {
    const checkbox = card.querySelector('.cb.checkbox');
    const categoryIdOpt = card.querySelector('.opt');
    const optionLabel = card.querySelector('label .OptionName');

    categoryIdOpt.setAttribute("id", category.categoryId);
    optionLabel.textContent = category.categoryName;
    categoryIdOpt.addEventListener("click", handleChange);
}
const importCategories = async () => {

    const response = await fetch(
        'api/categories'
    )

    if (!response.ok) {
        return [];
    }

    const data = await response.json();
    arr = data;
    drawCategories(data)
}

window.addEventListener("load", importCategories);
