<template>
    <div class="container">
        <div class="row justify-content-center align-items-center vh-100">
            <div class="col-md-6">
                <div class="d-flex flex-column align-items-center">
                    <div class="login-title text-center mb-2">Unlock the control with</div>
                    <img :src="nhsLogo" class="login-form-logo" alt="IMS" />
                    <div class="login-title-blue">IMS</div>
                    <div class="login-title mb-4 text-success">MFA Login</div>
                </div>

                <form @submit.prevent="onSubmit">
                    <div class="form-group">
                        <div class="text-center pb-2">MFA Code</div>
                        <div class="otp-input-container">
                            <input type="tel"
                                   class="digit-box"
                                   v-for="(digit, index) in otpDigits"
                                   :key="index"
                                   v-model="otpDigits[index]"
                                   :autofocus="index === 0"
                                   maxlength="1"
                                   @input="moveToNextInput(index)"
                                   @keydown.backspace="otpDigits[index] === '' && moveToPreviousInput(index)" />
                        </div>
                        <div id="validationServerUsernameFeedback"
                             class="invalid-feedback"
                             v-if="otpFailed">
                            Invalid code, double check!
                        </div>
                    </div>
                    <div class="form-group d-flex justify-content-between mt-4">
                        <button type="button" class="btn btn-danger basic w-25" @click="cancelMfa">Cancel</button>
                        <button type="button" class="btn btn-primary basic w-50" @click="onSubmit">
                            Fully login
                        </button>
                    </div>
                    <div class="text-center pt-2">
                        <div>
                            Your code has been sent to email:
                            <strong>u1953937@unimail.hud.ac.uk</strong>
                        </div>
                    </div>
                    <div class="text-center pt-2">
                        <div>
                            Your code will expire in:
                            <strong>{{ remainingTimeFormatted }}</strong>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
</template>

<script>
        import nhsLogo from "../assets/images/nhs_logo.svg";
        import axios from "axios";

    export default {
        data() {
            const staff = this.$store.state.user ? this.$store.state.user : null;
                return {
                    nhsLogo: nhsLogo,
                    info: null,
                    otpFailed: false,
                    otpDigits: Array(4).fill(""),
                    remainingTime: 10 * 60,
                    staff: staff,
                };
             },
            mounted() {
                this.startCountdown();
            },
            beforeDestroy() {
            clearInterval(this.countdownInterval);
            },
            methods: {
                moveToNextInput(index) {
                    const inputs = this.$el.querySelectorAll(".digit-box");
                    if (index < inputs.length - 1) {
                        inputs[index + 1].focus();
                    }
                },
                moveToPreviousInput(index) {
                    const inputs = this.$el.querySelectorAll(".digit-box");
                    if (index > 0) {
                        inputs[index - 1].focus();
                        inputs[index - 1].select();
                    }
                },
                startCountdown() {
                    this.countdownInterval = setInterval(() => {
                        if (this.remainingTime > 0) {
                            this.remainingTime--;
                        } else {
                            clearInterval(this.countdownInterval);
                        }
                    }, 1000);
                },
                cancelMfa() {
                    this.$router.push({ name: "Login" });
                },
                async onSubmit() {
                    const otp = this.otpDigits.join("");
                    alert(otp);
                    alert(this.staff);
                    try {
                        const response = await axios.post("/api/account/verifymfa", {
                            staffId: this.staff.staffId,
                            otp: otp,
                        });

                        if (response.data.success) {
                            this.otpFailed = false;
                            this.$store.commit("setUser", {
                                user: this.staff,
                                isFullyAuth: true,
                            });
                            this.$router.push({ name: "Home" }); // Redirect to Home Page
                        } else {
                            this.loginFailed = true;
                            console.error("OTP verification failed: ", response.data.error);
                        }
                    } catch (error) {
                        this.loginFailed = true;
                        this.error = "Error occurred while verifying OTP.";
                    }
                }, 
            },
            computed: {
                remainingTimeFormatted() {
                  const minutes = Math.floor(this.remainingTime / 60);
                  const seconds = this.remainingTime % 60;
                  return `${minutes}:${seconds.toString().padStart(2, "0")} minutes`;
                },
        },
    };
</script>

<style lang="scss" scoped>
    @import "../sass/app.scss";

    .login-title {
        font-size: 2.5rem;
    }

    .login-title-blue {
        font-size: 3rem;
        color: $main-blue;
        font-weight: bolder;
    }

    .login-form-logo {
        width: 100%;
        height: auto;
        max-width: 120px;
        margin: -35px;
    }

    form {
        width: 300px;
        margin: auto;
    }

    .link-primary {
        color: $main-blue;
    }

    .otp-input-container {
        display: flex;
        justify-content: center;
        margin-bottom: 16px;
    }

    .digit-box {
        height: 4rem;
        width: 3rem;
        border: 2px solid $main-blue-bright;
        display: inline-block;
        border-radius: 5px;
        margin: 0 5px;
        padding: 0 15px;
        font-size: 1.5rem;
        text-align: center;
        box-sizing: border-box;
    }

    .digit-box:focus {
        outline: 3px solid $main-blue;
    }
</style>