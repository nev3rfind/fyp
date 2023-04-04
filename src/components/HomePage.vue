<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="col-xl-4 col-sm-6 col-12">
                    <div class="card mb-2 main-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="media d-flex">
                                    <div class="media-body">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <h4>Hello, John Canbers</h4>
                                            <h4><span class="badge rounded-pill bg-success">Full access</span></h4>
                                        </div>
                                        <span>welcome to the <span class="fw-bold text-success">doctor</span> page</span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 col-sm-6 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="media d-flex">
                                    <div class="media-body text-right">
                                        <span>
                                            <i class='bx bxs-info-square card-icon'></i>
                                            Last logon at 16:56:20 13/10/2023 with
                                            <span class="badge ms-2 rounded-pill bg-success">MFA</span>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 col-sm-6 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="media d-flex align-items-center">
                                    <div class="media-body">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <h5 class="me-4">Patients waiting prescription</h5>
                                            <h5><a href="#">View all</a></h5>
                                        </div>
                                        <div class="d-flex flex-column mt-3">
                                            <span class="text-muted mb-2">24 completed</span>
                                            <div class="row progress-container">
                                                <div class="col-9">
                                                    <div class="progress w-100">
                                                        <div class="progress-bar bg-success" role="progressbar" style="width: 50%;" aria-valuenow="50" aria-valuemin="0" aria-valuemax="100">
                                                            <span class="progress-label">70%</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-3 text-end progress-label-container">
                                                    <a href="#" class="mt-2">20%</a>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <h5 class="me-4">Appointments history</h5>
                                <apexchart type="bar" :options="chartOptions" :series="chartSeries"></apexchart>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 col-sm-6 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <h5 class="me-4">Upcoming appointments</h5>
                                <div class="table-responsive">
                                    <table class="mt-2 table table-centered table-hover table-borderless">
                                        <thead>
                                            <tr>
                                                <th scope="col">#</th>
                                                <th scope="col">Patient Name</th>
                                                <th scope="col">Appointment Time</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(appointment, index) in formattedAppointments" :key="appointment.id">
                                                <td scope="row">{{ index + 1 }}</td>
                                                <td>{{ appointment.patientName }}</td>
                                                <td>{{ appointment.appointmentTime }}</td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 col-sm-6 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4">Total patients:</h5>
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-main dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
                                            109 patients assigned
                                        </button>
                                        <ul class="dropdown-menu scrollable-menu">
                                            <li><a class="dropdown-item" href="#">Action</a></li>
                                            <li><a class="dropdown-item" href="#">Another action</a></li>
                                            <li><a class="dropdown-item" href="#">Something else here</a></li>
                                            <li><hr class="dropdown-divider"></li>
                                            <li><a class="dropdown-item" href="#">Separated link</a></li>
                                            <li><a class="dropdown-item" href="#">Action</a></li>
                                            <li><a class="dropdown-item" href="#">Another action</a></li>
                                            <li><a class="dropdown-item" href="#">Something else here</a></li>
                                            <li><a class="dropdown-item" href="#">Action</a></li>
                                            <li><a class="dropdown-item" href="#">Another action</a></li>
                                            <li><a class="dropdown-item" href="#">Something else here</a></li>
                                        </ul>
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
    import VueApexCharts from "vue3-apexcharts";
    export default {
        components: {
            apexchart: VueApexCharts,
        },
        methods: {
            formatDate(date) {
                return moment(date).format("HH:mm:ss DD/MM/YYYY");
            },
        },
        computed: {
            user() {
                return this.$store.state.user;
            },
            formattedAppointments() {
                return this.appointments.map((appointment) => {
                    const date = new Date(appointment.appointmentTime);
                    const formattedDate = `${date.getHours().toString().padStart(2, "0")}:${date.getMinutes().toString().padStart(2, "0")} ${date.getDate().toString().padStart(2, "0")}/${(date.getMonth() + 1).toString().padStart(2, "0")}/${date.getFullYear().toString().substr(-2)}`;
                    return { ...appointment, appointmentTime: formattedDate };
                });
            },
        },

        data() {
            return {
                appointments: [
                    {
                        id: 1,
                        patientName: "John Doe",
                        appointmentTime: "2023-03-24T09:00:00",
                    },
                    {
                        id: 2,
                        patientName: "Jane Smith",
                        appointmentTime: "2023-03-25T10:30:00",
                    },
                    {
                        id: 3,
                        patientName: "Michael Johnson",
                        appointmentTime: "2023-03-26T11:15:00",
                    },
                    {
                        id: 4,
                        patientName: "Emma Brown",
                        appointmentTime: "2023-03-27T13:45:00",
                    },
                ],
                chartOptions: {
                    chart: {
                        // Columns appearing animation
                        animations: {
                            enabled: true,
                            easing: 'easeinout',
                            speed: 800,
                            animateGradually: {
                                enabled: true,
                                delay: 150
                            },
                            dynamicAnimation: {
                                enabled: true,
                                speed: 350
                            }
                        },
                        height: 280,
                        type: "bar",
                        toolbar: {
                            show: false,
                        },
                    },
                    plotOptions: {
                        bar: {
                            horizontal: false,
                        },
                    },
                    xaxis: {
                        categories: this.generateDaysArray(),
                        title: {
                            text: "Day",
                        },
                    },
                    yaxis: {
                        title: {
                            text: "Appointments",
                        },
                    },
                    tooltip: {
                        y: {
                            formatter: function (val) {
                                return val + " appointments";
                            },
                        },
                    },
                    colors: ["#1E90FF"],
                },
                chartSeries: [
                    {
                        name: "Appointments",
                        data: this.generateRandomData(),
                    },
                ],
            };
        },
        methods: {
            generateDaysArray() {
                const currentDate = new Date();
                const startDay = currentDate.getDate() - 13;
                const daysArray = Array.from({ length: 14 }, (_, i) => {
                    const day = new Date(currentDate.getFullYear(), currentDate.getMonth(), startDay + i);
                    return day.getDate();
                });
                return daysArray;
            },
            generateRandomData() {
                const randomData = Array.from({ length: 14 }, () => Math.floor(Math.random() * 15) + 1);
                return randomData;
            },
        },
    };
</script>

<style lang="scss" scoped>
    @import "../sass/app.scss";
    h4 {
        padding-right: 40px;
    }
    .card-icon {
        font-size: 1.5rem;
        color: $main-blue-bright;
        padding-right: 10px;
        position: relative;
        top: 5px;
    }
    a {
        color: $main-blue-bright;
        text-decoration: none;
        font-size: 1rem;
        text-transform:uppercase;
    }
    a:hover {
        color: $main-blue;
        text-decoration: underline;
    }

    .card {
        border: none;
        border-radius: 10px;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
    }

    .main-card {
        border: 1px solid $main-blue-bright;
    }

    .progress-container {
        align-items: center;
    }
    table th {
        text-transform: uppercase;
        font-size: 0.7rem;
    }
    table th, td {
        color: $neutral-black;
    }
    .table-borderless > tbody > tr {
        border-bottom: 1px solid $main-blue-bright;
    }
    .table-responsive {
        max-height: 200px;
        overflow-y: scroll;
    }
</style>