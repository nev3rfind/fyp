<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="col-xl-3 col-sm-6 col-12">
                    <div class="card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="media d-flex">
                                    <div class="align-self-center">
                                        <i class="bx bxs-capsule"></i>
                                    </div>
                                    <div class="media-body text-right">
                                        <h3>{{ user.fullName }}</h3>
                                        <span>{{ formatDate(user.lastLogin) }}</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import moment from 'moment';
    export default {
        methods: {
            formatDate(date) {
                return moment(date).format("HH:mm:ss DD/MM/YYYY");
            },
        },
        computed: {
            user() {
                return this.$store.state.user;
            },
        },
    };
</script>

<style lang="scss" scoped>

</style>