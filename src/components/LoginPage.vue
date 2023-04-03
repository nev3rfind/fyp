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
                        <input type="text" class="form-control" id="username" v-model="username" />
                    </div>
                    <div class="form-group mt-2">
                        <label for="password" class="form-label d-flex justify-content-between">
                            <span>Password</span>
                            <a class="link-primary" href="/forgot-password">Forgot Password?</a>
                        </label>
                        <input type="password" class="form-control" id="password" v-model="password" />
                    </div>
                    <div class="form-group text-center mt-4">
                        <button type="submit" class="btn btn-primary basic w-100 w-md-auto">Login</button>
                    </div>
                    <div class="text-center pt-2">
                        <div>Don't have access? <a href="#" class="link-primary">Request here</a></div>
                    </div>
                </form>
            </div>
            <p>data:  {{ info }}</p>
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
            };
        },
        methods: {
            async onSubmit() {
                try {
                    const response = await axios.post("/api/account/login", {
                        username: this.username,
                        password: this.password,
                    });

                    if (response.data.success) {
                        this.info = response.data.user
                        console.log(response);
                        this.$store.commit("setUser", response.data.user);
                        this.$router.push({ name: "Dashboard" });
                    } else {
                        this.error = response.data.error;
                    }
                } catch (error) {
                    console.log("Error trying to log in!")
                    this.error = "Error occurred while logging in.";
                }
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