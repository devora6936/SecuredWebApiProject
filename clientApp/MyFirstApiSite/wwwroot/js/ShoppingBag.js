


const removeFromBasket = (obj) => {
    let basket = JSON.parse(sessionStorage.getItem("basket")) || [];

    basket = basket.map(item => {
        if (item.product.productId === obj.product.productId) {
            item.quantity -= 1;
        }
        return item;
    }).filter(item => item.quantity > 0);

    sessionStorage.setItem("basket", JSON.stringify(basket));
    drawBasket();
};

const drawBasket = () => {
    const basket = JSON.parse(sessionStorage.getItem("basket")) || [];
    const template = document.getElementById('temp-row');
    const bodyOfTable = document.getElementById("bodyOfTable");
    const totalAmount = document.getElementById("totalAmount");
    const itemCount = document.getElementById("itemCount");

    bodyOfTable.innerHTML = '';

    let totalSum = 0;
    let totalItems = 0;

    basket.forEach(obj => {
        totalSum += obj.product.price * obj.quantity;
        totalItems += obj.quantity;

        const row = template.content.cloneNode(true);
        row.querySelector('.image').style.backgroundImage = `url('${obj.product.imageUrl}')`;
        row.querySelector('.itemName').textContent = obj.product.productName;
        row.querySelector('.itemNumber').textContent = obj.product.productNumber;
        row.querySelector('.price').textContent = (obj.product.price * obj.quantity).toFixed(2);
        row.querySelector('.amount').textContent = obj.quantity;
        row.querySelector('.DeleteButton').addEventListener("click", () => removeFromBasket(obj));

        bodyOfTable.appendChild(row);
    });

    itemCount.textContent = totalItems;
    totalAmount.textContent = totalSum.toFixed(2);
};


const placeOrder = async () => {
    const basket = JSON.parse(sessionStorage.getItem("basket")) || [];
    const user = JSON.parse(sessionStorage.getItem("user"));

    if (!user) {
        alert('User not found in session');
        return;
    }

    const orderItems = basket.map(item => ({
        productId: item.product.productId,
        quantity: item.quantity
    }));

    const orderData = {
        OrderDate: new Date(),
        OrderSum: parseFloat(document.getElementById("totalAmount").textContent),
        UserId: user.userId,
        OrderItems: orderItems
    };

    try {
        const response = await fetch('api/orders', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(orderData)
        });

        if (response.ok) {
            const order = await response.json();
            alert(`Order with id: ${order.orderId} placed successfully`);
            sessionStorage.removeItem("basket");
            window.location.href = "Products.html";
        } else {
            alert("Error placing order. Please try again.");
        }
    } catch (error) {
        console.error('Error placing order:', error);
        alert("Error placing order. Please try again.");
    }
};


window.addEventListener("load", () => {
    drawBasket();
});