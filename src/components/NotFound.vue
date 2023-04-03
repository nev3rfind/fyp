<template>
    <div class="container d-flex flex-column justify-content-center align-items-center vh-100">
        <h1 class="display-3 mb-3 text-danger">Error 404</h1>
        <p class="lead mb-4 text-secondary">Page not found</p>
        <button class="btn btn-main-blue text-white" @click="goBack">Go Back</button>
    </div>
</template>

<script>
    export default {
        methods: {
            goBack() {
                this.$router.go(-1);
            },
        },
    };
</script>

<style lang="scss" scoped>
</style>