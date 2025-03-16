export class Auth {
    async login (username, password) {
        try {
            let response = await fetch("/auth/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ username, password })
            });

            if (!response.ok) {
                throw new Error("Login failed");
            }

            return { success: true, message: "Login successful" };
        } catch (error) {
            return { success: false, message: error.message };
        }
    }
}

window.Login2 = Login2;