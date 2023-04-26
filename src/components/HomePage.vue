<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="col-xl-4 col-sm-12 col-12">
                    <div class="card mb-2 main-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h4>Hello, {{ user.fullName}}</h4>
                                    <h4>
                                        <span v-if="user.isFullyAuth" class="badge rounded-pill bg-success">Full access</span>
                                        <span v-else class="badge rounded-pill bg-danger">Only view</span>
                                    </h4>
                                </div>
                                <span v-if="user.isDoctor">welcome to the <span class="fw-bold text-success">doctor</span> page</span>
                                <span v-else="user.isNurse">welcome to the <span class="fw-bold text-info">nurse</span> page</span>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 col-sm-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <span>
                                    <i class='bx bxs-info-square card-icon'></i>
                                    Last logon at {{ formatDate(user.lastLogin) }} with
                                    <span class="badge ms-2 rounded-pill bg-success">{{ user.lastAuthenticated}}</span>
                                </span>       
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-4 col-sm-12 col-md-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4">Patients waiting prescription</h5>
                                    <h5><a href="#" @click.prevent="viewAllPrescriptions">View all</a></h5>
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
                <div class="col-xl-6 col-sm-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <h5 class="me-4">Upcoming appointments</h5>
                                <h6 v-if="isObjectEmpty(upcomingAppointments)">No upcoming appointments found</h6>
                                <div v-else class="table-responsive">
                                    <table class="mt-2 table table-centered table-borderless">
                                        <thead>
                                            <tr>
                                                <th scope="col">#</th>
                                                <th scope="col">Patient Name</th>
                                                <th scope="col">Appoint. Time</th>
                                                <th class="ps-5 text-end" scope="col">Status</th>
                                            </tr>
                                        </thead>
                                        <tbody>
                                            <tr v-for="(appointment, index) in upcomingAppointments" :key="appointment.AppointmentId">
                                                <td scope="row">{{ index + 1 }}</td>
                                                <td>{{ appointment.PatientFullName }}</td>
                                                <td>{{ formatAppointmentDate(appointment.AppointmentDate) }}</td>
                                                <td class="text-end align-middle">
                                                    <span v-if="appointment.Status === 'Scheduled'" class="badge rounded-pill bg-success table-badge">{{ appointment.Status }}</span>
                                                    <span v-else-if="appointment.Status === 'Cancelled'" class="badge rounded-pill bg-danger table-badge">{{ appointment.Status }}</span>
                                                    <span v-else-if="appointment.Status === 'Attended'" class="badge rounded-pill bg-info table-badge">{{ appointment.Status }}</span>
                                                    <span v-else="appointment.Status === 'Not Attended'" class="badge rounded-pill bg-warning table-badge">{{ appointment.Status }}</span>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-xl-6 col-sm-12 col-12">
                    <div class="card mb-2">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="d-flex justify-content-between align-items-center">
                                    <h5 class="me-4">Total patients:</h5>
                                    <div class="btn-group">
                                        <button type="button" class="btn btn-main dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
                                            {{ patientsCount }} patients assigned
                                        </button>
                                        <ul class="dropdown-menu scrollable-menu">
                                            <li><a class="dropdown-item" href="#">{{ patientsCount }} total</a></li>
                                            <li><hr class="dropdown-divider"></li>
                                            <li v-for="(patient, index) in patients" :key="patient.PatientId"><a class="dropdown-item" href="#" @click="goToPatientDetails(patient.PatientId)">{{ patient.fullName }}</a></li>
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
        },

        data() {
            return {
                prescriptions: [],
                groupedAppointments: [],
                upcomingAppointments: [],
                patients: [],
                patientsCount: 0,
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
            this.fetchUpcomingAppointments();
            this.fetchStaffPatients();
        },
        methods: {
            formatDate(date) {
                return moment(date).format("HH:mm:ss DD/MM/YYYY");
            },
            formatAppointmentDate(date) {
                return moment(date).format("HH:mm DD/MM");
            },
            isObjectEmpty(obj) {
                return Object.keys(obj).length === 0;
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
            // Get Upcoming appointments
            async fetchUpcomingAppointments() {
                try {
                    const response = await axios.post(`/api/appointment/GetUpcomingAppointmentsByStaffId?staffId=${this.user.staffId}`)
                    if (response.data.success) {
                        console.log(response.data.upcomingAppointments);
                        this.upcomingAppointments = response.data.upcomingAppointments;

                    } else {
                        this.upcomingAppointments = null;
                        console.log('Failed to get upcoming appointments');
                    }
                } catch (error) {
                    console.error('Error fetching upcoming appointments:', error);
                }
            },
            // Get Patients assigned to the current logged in staff
            async fetchStaffPatients() {
                try {
                    const response = await axios.post(`/api/patient/GetPatientsByStaffId?StaffId=${this.user.staffId}`)
                    if (response.data.success) {
                        console.log(response.data);
                        this.patients = response.data.patients;
                        this.patientsCount = response.data.patientsCount;

                    } else {
                        this.patients = null;
                    }
                } catch (error) {
                    console.error('Error fetching patients:', error);
                }
            },
            viewAllPrescriptions() {
                this.$router.push('/prescription');
            },
            // On patient click - go to patient details
            goToPatientDetails(patientId) {
                this.$router.push({
                    name: "PatientDetails",
                    params: {
                        patientId: patientId,
                        belongsToStaff: true,
                    },
                });
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
    .table-badge {
        font-size: 14px;
        font-weight: 500;
        color: $neutral-white;
    }
    tr:hover {
        color: $neutral-black;
        background-color: $neutral-grey-pale;
        cursor: pointer;
    }
</style>