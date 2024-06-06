

const getInputValueById = (id) => document.getElementById(id).value;
const showAlert = (message) => alert(message);

const fetchApi = async (url, method, body) => {
    try {
        const response = await fetch(url, {
            method,
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(body)
        });
        const result = await response.json();
        if (!response.ok) throw new Error(result.message || 'Error');
        return result;
    } catch (error) {
        throw error;
    }
}

const checkPassword = async () => {
    const password = getInputValueById('regPassword');
    try {
        const res = await fetchApi('api/users/checkPassword', 'POST', password);
        const progress = document.getElementById('progress');
        progress.value = JSON.stringify(res);
    } catch (error) {
        // Handle specific error if needed
    }
}

const login = async () => {
    const postData = {
        email: getInputValueById('email'),
        password: getInputValueById('password')
    };
    try {
        const res = await fetchApi('api/users/login', 'POST', postData);
        sessionStorage.setItem("user", JSON.stringify(res));
        window.location.href = "homePage.html";
    } catch (error) {
        showAlert("שם משתמש או סיסמא אינם תקינים");
    }
};

const register = async () => {
    const postData = {
        Email: getInputValueById('regEmail'),
        Password: getInputValueById('regPassword'),
        FirstName: getInputValueById('firstName'),
        LastName: getInputValueById('lastName')
    };
    try {
        await fetchApi('api/users', 'POST', postData);
        showAlert("user added");
        window.location.replace("home.html")
    } catch (error) {
        showAlert("fields are not valid");
    }
};

const update = async () => {
    const putData = {
        Email: getInputValueById('email'),
        Password: getInputValueById('password'),
        FirstName: getInputValueById('firstName'),
        LastName: getInputValueById('lastName')
    };
    try {
        const user = JSON.parse(sessionStorage.getItem('user'));
        if (!user || !user.userId) {
            showAlert('User not found in session');
            return;
        }

        const userId = user.userId;
        const url = `api/users/${userId}`;

        const res = await fetchApi(url, 'PUT', putData);
        showAlert("user updated");
        console.log('Update response:', res);
    } catch (error) {
        showAlert("fields are not valid");
    }
};