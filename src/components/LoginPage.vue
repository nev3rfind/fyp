<template>
    <div class="container">
        <div class="row justify-content-center align-items-center  vh-100">
            <div class="col-md-6">
                <div class="d-flex flex-column align-items-center">
                    <div class="login-title text-center mb-2">Unlock the control with</div>
                    <img :src="nhsLogo" class="login-form-logo" alt="IMS" />
                    <div class="login-title-blue">IMS</div>
                    <div class="login-title mb-4">Login</div>
                </div>
                
                <form @submit.prevent="onSubmit">
                    <div class="form-group">
                        <label class="form-label" for="username">Staff Username</label>
                        <input type="text" :class="usernameClass" id="username" v-model="username" />
                    </div>
                    <div class="form-group mt-2">
                        <label for="password" class="form-label d-flex justify-content-between">
                            <span>Password</span>
                            <a class="link-primary" href="/forgot-password">Forgot Password?</a>
                        </label>
                        <input type="password" :class="passwordClass" id="password" v-model="password" />
                        <div id="validationServerUsernameFeedback" class="invalid-feedback" v-if="loginFailed">
                            Invalid username or password
                        </div>
                        <div id="validationServerUsernameFeedback" class="invalid-feedback" v-if="mfaLoginFailed">
                            Fail generating OTP code
                        </div>
                    </div>
                    <div class="form-group text-center mt-4">
                        <button type="submit" class="btn btn-warning basic w-100 w-md-auto">Login with Pass (only view)</button>
                    </div>
                    <div class="form-group text-center mt-2">
                        <button type="button" class="btn btn-primary basic w-100 w-md-auto" @click="onMfaLogin">Login with MFA (full access)</button>
                    </div>
                    <div class="text-center pt-2">
                        <div>Don't have access? <a href="#" class="link-primary">Request here</a></div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
    import nhsLogo from '../assets/images/nhs_logo.svg';
    import axios from 'axios';
    export default {
        data() {
            return {
                nhsLogo: nhsLogo,
                username: "",
                password: "",
                error: "",
                info: null,
                loginFailed: false,
                otpFailed: false,
                mfaLoginFailed: false,
            };
        },
        methods: {
            // Login with pass
            async onSubmit() {
                try {
                    const response = await axios.post("/api/account/login", {
                        username: this.username,
                        password: this.password,
                    });

                    if (response.data.success) {
                        this.loginFailed = false;
                        this.info = response.data.user
                        this.$store.commit("setUser", {
                            user: response.data.user,
                            isFullyAuth: false,
                        });
                        this.$router.push({ name: "Home" }); // Redirect to Home Page
                    } else {
                        this.loginFailed = true;
                        console.error("Login failed: ", response.data.error);
                    }
                } catch (error) {
                    this.info = error;
                    this.loginFailed = true;
                    this.error = "Error occurred while logging in.";
                }
            },
            // MFA
            async onMfaLogin() {
                try {
                    const response = await axios.post("/api/account/login", {
                        username: this.username,
                        password: this.password,
                    });

                    if (response.data.success) {
                        this.mfaLoginFailed = false;
                        alert(response.data.user.staffId)
                        const otpStatus = await axios.post("/api/account/mfalogin", {
                            staffId: response.data.user.staffId,
                        });
                        if (otpStatus.data.success) {
                            this.$store.commit("setUser", {
                                user: otpStatus.data.user,
                                isFullyAuth: false,
                            });
                            alert(otpStatus.data.success)
                            this.mfaLoginFailed = false;
                            this.$router.push({ name: "Mfa" }); // Redirect to MFA Page
                        } else {
                            alert("otp status unsuccess")
                            this.mfaLoginFailed = true;
                        }
                    } else {
                        this.loginFailed = true; // Update this line
                        console.error("Login failed: ", response.data.error);
                    }
                } catch (error) {
                    this.mfaLoginFailed = true; // Update this line
                    this.error = "Error occurred while logging in.";
                }
            },
        },
        computed: {
            usernameClass() {
                return (this.loginFailed || this.mfaLoginFailed) ? 'form-control is-invalid' : 'form-control';
            },
            passwordClass() {
                return (this.loginFailed || this.mfaLoginFailed) ? 'form-control is-invalid' : 'form-control';
            },
        },

    };

</script>

<style lang="scss" scoped>
    @import "../sass/app.scss";
    .login-title{
        font-size: 2.5rem;
    }
    .login-title-blue {
        font-size: 3rem;
        color: $main-blue;
        font-weight:bolder;
    }
    .login-form-logo {
        width: 100%;
        height: auto;
        max-width: 120px;
        margin: -35px;
    }

    form {
        width: 300px;
        margin:auto;
    }
    .link-primary {
        color: $main-blue;
    }
</style>