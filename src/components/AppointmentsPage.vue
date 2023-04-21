<template>
    <div class="main-content content-page">
        <div class="container">
            <div class="row">
                <div class="title-container d-flex p-1 flex-column m-auto text-center pb-4">
                    <div class="px-2">
                        <h1>Appointments <i class='bx bxs-calendar'></i></h1>
                    </div>
                </div>
                <div class="col-xl-12 col-sm-12 col-md-12 col-12 pb-4">
                    <div class="card mb-2 title-card">
                        <div class="card-content">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-6">
                                        <label for="start-date" class="form-label">Date range from</label>
                                        <input type="date" class="form-control border" id="start-date" v-model="startDate">
                                    </div>
                                    <div class="col-6">
                                        <label for="end-date" class="form-label">Date range to</label>
                                        <input type="date" class="form-control border" id="end-date" v-model="endDate">
                                    </div>
                                </div>
                            </div>
                        </div>
                        <button class="btn confirm-btn w-100 appointment-btn" @click="showNewAppointmentCard = true"><i class='bx bx-calendar-plus me-2'></i> Schedule new appointment</button>
                    </div>
                </div>
            </div>
            <!-- New appointment card -->
            <transition name="slide-fade" mode="out-in">
                <div class="row" v-if="showNewAppointmentCard" key="new-appointment-row">
                    <div class="col-xl-12 col-sm-12 col-12">
                        <div class="card mb-2 add-appointment-card">
                            <div class="card-content">
                                <div class="card-body" v-if="!appointmentAdded">
                                    <h5 class="mb-3"><i class='bx bxs-calendar-plus text-success'></i> Schedule new appointment</h5>
                                    <div class="mb-3">
                                        <label for="patient-select" class="form-label">Patient</label>
                                        <select class="form-select border" id="patient-select" v-model="selectedPatient">
                                            <option disabled :value="null">Please select a patient</option>
                                            <option v-for="patient in patients" :key="patient.PatientId" :value="patient.PatientId">
                                                {{ patient.fullName }}
                                            </option>
                                        </select>
                                        <div v-if="patientError" class="invalid-feedback">{{ patientError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="appointment-date" class="form-label">Appointment Date</label>
                                        <input type="date" :class="{ 'is-invalid': dateError }" class="form-control border" id="appointment-date" v-model="appointmentDate">
                                        <div v-if="dateError" class="invalid-feedback">{{ dateError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="appointment-name" class="form-label">Appointment Name</label>
                                        <input type="text" :class="{ 'is-invalid': nameError }" class="form-control border" id="appointment-name" v-model="appointmentName">
                                        <div v-if="nameError" class="invalid-feedback">{{ nameError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="appointment-description" class="form-label">Appointment Description</label>
                                        <textarea :class="{ 'is-invalid': descriptionError }" class="form-control border" id="appointment-description" v-model="appointmentDescription"></textarea>
                                        <div v-if="descriptionError" class="invalid-feedback">{{ descriptionError }}</div>
                                    </div>
                                    <div class="d-flex buttons-row">
                                        <button class="btn btn-sm btn-success w-50" @click="submitAppointment">Confirm</button>
                                        <button class="btn btn-sm btn-danger w-25 ms-2 me-2 text-dark" @click="cancelAddAppointment">Cancel</button>
                                    </div>
                                </div>
                                <div class="card-body d-flex justify-content-center align-items-center" v-else>
                                    <h5 class="text-center text-success">Appointment added <i class='bx bxs-check-circle'></i></h5>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </transition>
            <div class="row scrollable-container">
                <!-- Appointment card -->
                <transition name="flip-right" mode="out-in" v-if="appointments !== 0" v-for="(appointment, index) in appointments" :key="appointment.AppointmentId">
                    <div v-if="!modifyingAppointment[appointment.AppointmentId]" class="col-xl-6 col-sm-12 col-md-12 col-12 mt-0">
                        <div class="card mb-2 appointment-card">
                            <div class="card-content">
                                <div class="card-body card-top ps-4">
                                    <div class="top-card-text mb-2">
                                        <span class="badge bg-success badge-app" v-if="appointment.Status === 'Attended'">{{ appointment.Status }}</span>
                                        <span class="badge bg-warning badge-app" v-else-if="appointment.Status === 'Scheduled'">{{ appointment.Status }}</span>
                                        <span class="badge bg-danger badge-app" v-else-if="appointment.Status === 'Cancelled'">{{ appointment.Status }}</span>
                                        <span class="badge bg-danger badge-app" v-else>{{ appointment.Status }}</span>
                                    </div>
                                    <div class="top-card-text"><i class='bx bxs-time time-icon'></i>{{ formatDate(appointment.AppointmentDate) }}</div>
                                </div>
                                <div class="card-body card-down ps-4">
                                    <div class="avatar-block">
                                        <div class="avatar-initials">{{ appointment.Initials }}</div>
                                    </div>
                                    <div class="card-down-content d-inline-block ps-3">
                                        <p class="mb-0"><strong>{{ appointment.PatientFullName }}</strong></p>
                                        <p class="mb-2">{{ formatDob(appointment.PatientDob) }}</p>
                                        <div class="mb-3"><span class="badge border bg-app-name"><i class='bx bxs-info-circle'></i>{{ appointment.AppointmentName }}</span></div>
                                    </div>
                                    <div class="d-flex buttons-row">
                                        <button class="btn btn-sm btn-success w-50" @click="startModifyingAppointment(appointment.AppointmentId)">Modify</button>
                                        <button class="btn btn-sm btn-warning w-25 ms-2 me-2 text-dark" @click="openCancelModal(appointment.AppointmentId)">Cancel</button>
                                        <button class="btn btn-sm btn-danger w-25">Delete</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div v-else class="col-xl-6 col-sm-12 col-md-12 col-12 mt-0">
                        <!-- Modify appointment card -->
                        <div class="card mb-2 appointment-card">
                            <div class="card-content">
                                <div class="card-body">
                                    <h5 class="mb-3">Modifying appointment #{{ appointment.AppointmentId }}</h5>
                                    <div class="mb-3">
                                        <label for="patient-select" class="form-label">Patient</label>
                                        <select class="form-select border" id="patient-select" v-model="selectedPatient">
                                            <option disabled :value="appointment.PatientFullName">{{ appointment.PatientFullName}}</option>
                                        </select>
                                    </div>
                                    <div class="mb-3">
                                        <label for="status-select" class="form-label">Appointment Status</label>
                                        <select class="form-select border" id="status-select" v-model="selectedStatus">
                                            <option value="Scheduled">Scheduled</option>
                                            <option value="Attended">Attended</option>
                                            <option value="Not attented">Not attented</option>
                                            <option value="Cancelled">Cancelled</option>
                                        </select>
                                    </div>
                                    <div class="mb-3">
                                        <label for="appointment-date" class="form-label">Appointment Date</label>
                                        <input type="date" :class="{ 'is-invalid': dateError }" class="form-control border" id="appointment-date" v-model="appointmentDate">
                                        <div v-if="dateError" class="invalid-feedback">{{ dateError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="appointment-name" class="form-label">Appointment Name</label>
                                        <input type="text" :class="{ 'is-invalid': nameError }" class="form-control border" id="appointment-name" v-model="appointmentName">
                                        <div v-if="nameError" class="invalid-feedback">{{ nameError }}</div>
                                    </div>
                                    <div class="mb-3">
                                        <label for="appointment-description" class="form-label">Appointment Description</label>
                                        <textarea :class="{ 'is-invalid': descriptionError }" class="form-control border" id="appointment-description" v-model="appointmentDescription"></textarea>
                                        <div v-if="descriptionError" class="invalid-feedback">{{ descriptionError }}</div>
                                    </div>
                                    <div class="d-flex justify-content-between buttons-row">
                                        <button class="btn btn-light btn-sm border confirm-btn" @click="cancelModifyAppointment(appointment.AppointmentId)">Cancel</button>
                                        <button class="btn btn-success btn-sm" @click="updateAppointment(appointment.AppointmentId)">Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </transition>
            </div>
        </div>
    </div>
    <!-- Cancel appointment modal -->
    <div class="modal fade" id="cancelAppointmentModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Cancel Appointment</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body text-center">
                    <p>Are you sure you want to cancel this appointment?</p>
                    <p class="fw-bold text-danger">Appointment status will reflect immediately.</p>
                </div>
                <div class="modal-footer d-flex justify-content-between buttons-row">
                    <button class="btn btn-light btn-sm border confirm-btn w-25"  data-bs-dismiss="modal">Cancel</button>
                    <button class="btn btn-success btn-sm w-50" @click="confirmCancelAppointment">Confirm</button>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from 'axios';
    import { Modal } from 'bootstrap';
    import moment from 'moment';

    export default {
        data() {
            const staffId = this.$store.state.user ? this.$store.state.user.staffId : null;
            const currentDate = new Date();
            const oneWeek = 7 * 24 * 60 * 60 * 1000;
            const startDate = new Date(currentDate.getTime() - oneWeek);
            const endDate = new Date(currentDate.getTime() + oneWeek);

            return {
                staffId: staffId,
                startDate: startDate.toISOString().split('T')[0],
                endDate: endDate.toISOString().split('T')[0],
                showNewAppointmentCard: false,
                patients: [],
                selectedPatient: null,
                selectedStatus: null,
                appointmentDate: null,
                appointmentName: '',
                appointmentDescription: '',
                patientError: null,
                dateError: null,
                nameError: null,
                descriptionError: null,
                appointmentAdded: false,
                modifyingAppointment: {},
                selectedAppointmentId: null,
                appointments: [],
                patients: [],
            };
        },
        async mounted() {
            await this.fetchAppointments();
        },
        watch: {
            startDate: {
                handler() {
                    this.fetchAppointments();
                },
            },
            endDate: {
                handler() {
                    this.fetchAppointments();
                },
            },
        },
        created() {
            this.fetchStaffPatients();
        },
        methods: {
            // Format appointment date
            formatDate(date) {
                return moment(date).format('LLLL');
            },
            formatDob(date) {
                return moment(date).format('DD/MM/YYYY');
            },
            showAddAppointment() {
                this.showNewAppointmentCard = true;
            },
            cancelAddAppointment() {
                this.showNewAppointmentCard = false;
            },
            async submitAppointment() {
                this.patientError = null;
                this.dateError = null;
                this.nameError = null;
                this.descriptionError = null;

                if (this.selectedPatient === 'null') {
                    this.patientError = 'Please select a patient';
                }

                if (!this.appointmentDate) {
                    this.dateError = 'Please select an appointment date';
                }

                if (!this.appointmentName) {
                    this.nameError = 'Please enter an appointment name';
                }

                if (!this.appointmentDescription) {
                    this.descriptionError = 'Please enter an appointment description';
                }

                if (!this.patientError && !this.dateError && !this.nameError && !this.descriptionError) {
                    // Submit the form, save the data, etc.
                    try {
                        const response = await axios.post('/api/appointment/AddAppointmentRecord', {
                            staffId: this.staffId,
                            patientId: this.selectedPatient,
                            appointmentDate: this.appointmentDate,
                            appointmentName: this.appointmentName,
                            description: this.appointmentDescription,
                        });
                        if (response.data.success) {
                            this.fetchAppointments();
                            this.appointmentAdded = true;
                            setTimeout(() => {
                                this.appointmentAdded = false;
                                this.showNewAppointmentCard = false;
                                this.resetForm();
                            }, 2000);
                        } else {
                            console.log('Error adding medication record');
                        }  
                    } catch (error) {
                        console.error(error);
                    }
                }
            },
            resetForm() {
                this.selectedPatient = null;
                this.appointmentDate = null;
                this.appointmentName = '';
                this.appointmentDescription = '';
            },
            cancelAddAppointment() {
                this.showNewAppointmentCard = false;
                this.resetForm();
            },
            startModifyingAppointment(appointmentId) {
                this.modifyingAppointment[appointmentId] = true;
                
            },
            cancelModifyAppointment(appointmentId) {
                delete this.modifyingAppointment[appointmentId];
            },
            openCancelModal(appointmentId) {
                this.selectedAppointmentId = appointmentId;
                const modal = new Modal(document.getElementById('cancelAppointmentModal'));
                modal.show();
            },
            // Get appointemnts by given date range
            async fetchAppointments() {
                try {
                    const response = await axios.post("/api/appointment/GetAppointmentsByRange", {
                        staffId: this.staffId,
                        startDate: this.startDate,
                        endDate: this.endDate,
                    });
                    if (response.data.success) {
                        console.log(response.data);
                        this.appointments = response.data.appointments;
                    } else {
                        console.log('Failed to get appointments:');
                    }
                } catch (error) {
                    console.error(error);
                }
            },
            // Get patients list for dropdown
            async fetchStaffPatients() {
                try {
                    const response = await axios.post(`/api/patient/GetPatientsByStaffId?StaffId=${this.staffId}`)
                    if (response.data.success) {
                        console.log(response.data);
                        this.patients = response.data.patients;

                    } else {
                        this.patients = null;
                    }
                } catch (error) {
                    console.error('Error fetching patients:', error);
                }
            }
        },
    };
</script>

<style lang="scss" scoped>
    @import "../sass/app.scss";

    .title-container {
        i {
            position: relative;
            top: 3px;
            color: $main-blue-nice;
        }
    }
    .appointment-btn {
        background: $main-blue-nice !important;
        border-radius: 20px;
        font-size: 1.2rem;
        color: $neutral-white;

        i {
            position: relative;
            top: 1px;
        }
    }

    .calendar-icon {
        font-size: 18px;
        color: $main-blue-bright;
        position: relative;
        top: 1px;
        padding-right: 5px;
    }

    .user-icon {
        position: relative;
        top: 3px;
        font-size: 20px;
        color: $green;
    }
    .avatar-initials {
        font-family: Nunito;
        background-color: $neutral-grey-mid;
        color: #FFFFFF;
        text-align: center;
        line-height: 0px;
        border-radius: 30%;
        font-size: 28px;
        font-family: Nunito;
        padding: 22px 16px;
    }

    .btn-primary {
        background-color: #0080ff;
        border-color: #0080ff;
    }

    .btn-primary:hover {
        background-color: #0073e6;
        border-color: #0073e6;
    }

    .btn-danger {
        background-color: #ff4d4d;
        border-color: #ff4d4d;
    }

    .btn-danger:hover {
        background-color: #ff1a1a;
        border-color: #ff1a1a;
    }

    .appointment-card {
        border-radius: 20px;
    }

    .card-top {
        background-color: $main-blue-nice;
        border-radius: 20px 20px 0px 0px;
    }

    .card-down {
        background-color: #FFFFFF;
        border-radius: 0px 0px 20px 20px;
    }

    .top-card-text {
        color: $neutral-white;
    }

    .badge-app {
        font-size: .85rem;
        font-weight: 500;
    }

    .top-card-text:nth-child(2) {
        font-size: .95rem;
    }
    .time-icon {
        font-size: 20px;
        padding-right: 5px;
        position: relative;
        top: 3px;
    }
    .avatar-block {
        display: inline-block;
        vertical-align: top;
    }

    .card-down-content {

        p:nth-child(1) {
            color: $neutral-grey-dark;
        }

        p:nth-child(2) {
            font-size: 0.9rem;
            color: $neutral-black;
            font-weight: bold;
        }
    }

    .bg-app-name {
        color: $neutral-black;
        background: $neutral-grey-pale;
        font-size: .8rem;
        padding-bottom: 7px;

        i {
            font-size: 18px;
            color: $main-blue-bright;
            position: relative;
            top: 3px;
            padding-right:5px;
        }
    }

    .buttons-row {

        button {
        text-transform: uppercase;
        border-radius: 15px;
        color: $neutral-white;
        padding: 8px;
        font-size: .8rem;
        }
        button:nth-child(1) {
            background: $main-blue-nice!important;
        }
    }

    .title-card {
        border-radius: 0 0 20px 20px;
    }

    .scrollable-container {
        max-height: calc(100vh - 450px);
        overflow-y: auto;
    }

    .content-page {
        overflow-y: clip!important;
    }

    .slide-fade-enter-active,
    .slide-fade-leave-active {
        transition: all 1s;
    }

    .slide-fade-enter-from,
    .slide-fade-leave-to {
        opacity: 0;
        transform: translateY(-30px);
    }

    .flip-right-enter-active,
    .flip-right-leave-active,
    .flip-left-enter-active,
    .flip-left-leave-active {
        transition: all 0.5s;
    }

    .flip-right-enter-from,
    .flip-left-leave-to {
        transform: translateX(100%);
        opacity: 0;
    }

    .flip-right-leave-to,
    .flip-left-enter-from {
        transform: translateX(-100%);
        opacity: 0;
    }

    .btn-close {
        border: none;
        background: transparent;
        color: black!important;
    }

</style>