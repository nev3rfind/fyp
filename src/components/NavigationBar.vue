<template>
    <div class="top-bar">
        <img :src="nhsLogo" class="top-logo" />
        <div class="icons">
            <i class='bx bxs-message-rounded-dots'></i>
            <i class='bx bxs-user-account'></i>
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
                    <a href="/index.html" class="main-nav-link">
                        <i class="bx bxs-home"></i>
                        <span class="menu-link">Home</span>
                    </a>
                </li>
                <li class="main-nav-item">
                    <a href="/index.html" class="main-nav-link">
                        <i class="bx bx-search-alt"></i>
                        <span class="menu-link">Patients search</span>
                    </a>
                </li>
                <li class="main-nav-item">
                    <a id="compareButton" href="#" class="main-nav-link">
                        <i class="bx bxs-capsule"></i>
                        <span class="menu-link">Prescription</span>
                    </a>
                </li>
                <li class="main-nav-item">
                    <a href="ranking.html" class="main-nav-link">
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
        },
    };
</script>

<style lang="scss" scoped>
  
</style>