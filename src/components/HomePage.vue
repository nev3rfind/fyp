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
                                            <h4>Hello, {{ user.fullName}}</h4>
                                            <h4><span class="badge rounded-pill bg-success">Full access</span></h4>
                                        </div>
                                        <span v-if="user.isDoctor">welcome to the <span class="fw-bold text-success">doctor</span> page</span>
                                        <span v-else="user.isNurse">welcome to the <span class="fw-bold text-info">nurse</span> page</span>
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
                                            Last logon at {{ formatDate(user.lastLogin) }} with
                                            <span class="badge ms-2 rounded-pill bg-success">{{ user.lastAuthenticated}}</span>
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
                                        <div class="d-flex flex-column mt-3" v-if="prescriptions.success">
                                            <span class="text-muted mb-2">{{ prescriptions.renewedPrescriptions}}/{{prescriptions.totalPrescriptions}} completed</span>
                                            <div class="row progress-container">
                                                <div class="col-9">
                                                    <div class="progress w-100">
                                                        <div class="progress-bar bg-success" role="progressbar" :style="'width: ' + prescriptions.renewedProcentage + '%'" aria-valuenow="{{ prescriptions.renewedProcentage }}" aria-valuemin="0" aria-valuemax="100">
                                                            <span class="progress-label">{{ prescriptions.renewedProcentage }} %</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-3 text-end progress-label-container">
                                                    <a href="#" class="mt-2">{{ 100 - prescriptions.renewedProcentage }} %</a>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex flex-column mt-3" v-else>
                                            <span class="text-muted mb-2">No records found</span>
                                            <div class="row progress-container">
                                                <div class="col-9">
                                                    <div class="progress w-100">
                                                        <div class="progress-bar bg-success" role="progressbar" style="width:0%" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100">
                                                            <span class="progress-label">0%</span>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-3 text-end progress-label-container">
                                                    <a href="#" class="mt-2">0%</a>
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
                                <h5 class="me-4">Appointments history (last 12 months)</h5>
                                <apexchart ref="chart" type="bar" :options="chartOptions" :series="chartSeries"></apexchart>
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
    import axios from 'axios';
    export default {
        components: {
            apexchart: VueApexCharts,
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
                prescriptions: [],
                groupedAppointments: [],
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
                        fontFamily: 'Roboto, sans-serif',
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
                        stacked: true,
                        stackType: '10%',
                        toolbar: {
                            show: false,
                        },
                    },
                    plotOptions: {
                        bar: {
                            dataLabels: {
                                enabled: false,
                            },
                            horizontal: false,
                        },
                    },
                    xaxis: {
                        categories: [],
                        title: {
                            text: "Month",
                            style: {
                                fontFamily: 'Roboto, sans-serif',
                            },
                        },
                    },
                    yaxis: {
                        title: {
                            text: "Appointments count",
                            style: {
                                fontFamily: 'Roboto, sans-serif',
                            },
                        },
                    },
                    tooltip: {
                        y: {
                            formatter: function (val) {
                                return val + " appointments";
                            },
                        },
                    },
                    colors: ["#0072CE"],
                },
                chartSeries: [
                    {
                        name: "Appointments",
                        data: [],
                    },
                ],
            };
        },
        created() {
            this.fetchPrescription();
            this.fetchGroupedAppointments();
        },
        methods: {
            formatDate(date) {
                return moment(date).format("HH:mm:ss DD/MM/YYYY");
            },
            // Getting prescription
            async fetchPrescription() {
                try {
                    const response = await axios.post(`/api/prescription/GetPatientsPrescriptionByStaffId?StaffId=${this.user.staffId}`);
                    if (response.data.success) {
                        console.log(response.data);
                       this.prescriptions = response.data;
                    } else {
                        console.log('Failed to get prescriptions');
                    }
                } catch (error) {
                    console.error('Error fetching prescriptions:', error);
                }
            },
            // Getting prescription
            async fetchGroupedAppointments() {
                try {
                    const response = await axios.post(`/api/appointment/GetAppointmentsByStaffId?staffId=${this.user.staffId}`);
                    if (response.data.success) {
                        console.log(response.data);
                        this.groupedAppointments = response.data.groupedAppointments;

                        // Update chart data with fetched data
                        const months = this.groupedAppointments.map(a => a.Month);
                        const count = this.groupedAppointments.map(a => a.Count);

                        this.$refs.chart.updateOptions({
                            xaxis: {
                                categories: months
                            },
                            series: [
                                {
                                    name: "Appointments",
                                    data: count
                                }
                            ]
                        });

                    } else {
                        console.log('Failed to get grouped appointments');
                    }
                } catch (error) {
                    console.error('Error fetching grouped appointments:', error);
                }
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