<template>
    <div class="main-content content-page">
        <div class="container">
            <h1>Hello, {{ user.fullName }}</h1>
        </div>
    </div>

</template>

<script>
    export default {
        // ...
        computed: {
            user() {
                return this.$store.state.user;
            },
        },
    };
</script>

<style lang="scss" scoped>

</style>