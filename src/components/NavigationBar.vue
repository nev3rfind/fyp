<template>
    <div class="top-bar">
        <img :src="nhsLogo" class="top-logo" />
        <div class="icons">
            <i class='bx bxs-message-rounded-dots'></i>
            <button class='btn'>
                <i class='bx bxs-user-account'></i>
                <ul class="dropdown">
                    <li>
                    <a href="#" @click.prevent="userSettings">
                        <i class='bx bx-cog'></i> User settings
                        </a>
                    </li>
                    <li>
                    <a href="#" @click.prevent="logOut">
                        <i class='bx bx-log-out-circle'></i> Log Out
                        </a>
                    </li>
                </ul>
            </button>
        </div>

    </div>
    <div class="wrapper">
        <div class="main-nav-bar">
            <a href="#" class="logo-custom text-center">
                <img :src="nhsLogo" alt="NHS">
            </a>
            <div class="logo-title"></div>
            <ul class="main-nav" ref="responsiveNav" :class="navClasses">
                <li class="main-nav-item">
                    <a href="#" @click.prevent="navigateToRoute('/')" class="main-nav-link">
                        <i class="bx bxs-home"></i>
                        <span class="menu-link">Home</span>
                    </a>
                </li>
                <li class="main-nav-item">
                    <a href="#" @click.prevent="navigateToRoute('/search')" class="main-nav-link">
                        <i class="bx bx-search-alt"></i>
                        <span class="menu-link">Patients search</span>
                    </a>
                </li>
                <li class="main-nav-item">
                    <a href="#" @click.prevent="navigateToRoute('/prescription')" class="main-nav-link">
                        <i class="bx bxs-capsule"></i>
                        <span class="menu-link">Prescription</span>
                    </a>
                </li>
                <li class="main-nav-item">
                    <a href="#" @click.prevent="navigateToRoute('/appointments')" class="main-nav-link">
                        <i class='bx bxs-calendar'></i>
                        <span class="menu-link">Appointments</span>
                    </a>
                </li>
            </ul>
        </div>
    </div>
   
</template>

<script>
    import nhsLogo from '../assets/images/nhs_logo.svg';
    export default {
        data() {
            return {
                navClasses: '',
                nhsLogo: nhsLogo,
            };
        },
        mounted() {
            this.updateSize();
            window.addEventListener('resize', this.updateSize);
        },
        beforeUnmount() {
            window.removeEventListener('resize', this.updateSize);
        },
        methods: {
            updateSize() {
                if (window.innerWidth < 1025) {
                    this.navClasses = 'd-flex justify-content-around';
                } else {
                    this.navClasses = '';
                }
            },
            toggleDropdown() {
                const dropdownElement = document.getElementById("accountDropdown");
                const dropdownInstance = new bootstrap.Dropdown(dropdownElement);
                dropdownInstance.toggle();
            },
            userSettings() {
                alert("Open user settings page");
            },
            logOut() {
                this.$store.commit("setUser", {
                    user: null,
                    isFullyAuth: false,
                });
                this.$router.push({ name: "Login" });
            },
            navigateToRoute(route) {
                this.$router.push(route);
            },

        },
    };
</script>

<style lang="scss" scoped>
    @import '../sass/app.scss';
   

</style>