<template>
    <div class="main-content content-page center-content">
        <div class="container">
            <div class="row">
                <div class="title-container d-flex p-3 flex-column m-auto text-center">
                    <div class="px-2">
                        <h1>Patient search <i class='bx bx-search-alt-2'></i></h1>
                    </div>
                    <div class="search-bar position-relative pb-4">
                        <div class="input-container">
                            <input id="searchBar" v-model="searchBar" type="text" autocomplete="off" class="form-control input-icon" placeholder="Search">
                            <i class='bx bx-search-alt search-icon'></i>
                        </div>
                        <ul id="patientsResults" v-if="searchBar.length >= 3">
                            <li v-if="!patientsResults || patientsResults.length === 0">No matching patients</li>
                            <li v-else v-for="(patient, index) in patientsResults" 
                                :key="patient.PatientId"
                                @click="goToPatientDetails(patient.PatientId, patient.belongsToStaff)"
                                >
                                {{ patient.fullName }}
                                <i
                                   v-if="patient.belongsToStaff"
                                   class="bx bxs-lock-open text-success"
                                   ></i>
                                <i
                                   v-else
                                   class="bx bxs-lock text-danger"
                                   ></i>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
    import axios from "axios";

    export default {
        data() {
            const staffId = this.$store.state.user ? this.$store.state.user.staffId : null;
            return {
                staffId: staffId,
                searchBar: "",
                patientsResults: [],
            }
        },
        watch: {
            searchBar(searchTerm) {
                if (searchTerm.length >= 3) {
                    this.searchPatients(searchTerm);
                } else {
                    this.patientsResults = [];
                }
            }
        },
        methods: {
            async searchPatients(searchTerm) {
                try {
                    const response = await axios.post("/api/patient/GetPatientsBySearchTerm", {
                        staffId: this.staffId,
                        searchTerm: searchTerm,
                    });

                    if (response.data.success) {
                        this.patientsResults = response.data.patients;
                        console.log(this.patientsResults);
                    } else {
                        this.patientsResults = [];
                    }
                } catch (error) {
                    console.error("Error fetch patients: ", error);
                    this.patientsResults = [];
                }
            },
            goToPatientDetails(patientId, belongsToStaff) {
             //   alert(patientId);
                this.$router.push({
                    name: "PatientDetails",
                    params: {
                        patientId: patientId,
                        belongsToStaff: belongsToStaff,
                    },
                });
            },
        },
    }

</script>

<style lang="scss">
    @import "../sass/app.scss";

    .center-content {
        height: 100vh;
        display: flex;
        align-items: center;
        justify-content: center;
        padding-bottom: 50%;
    }

    .title-container {
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 100%;

        h1 {
            margin-bottom: 30px;
        }
    }

    .search-bar .input-container {
        position: relative;
    }

    .search-bar input {
        width: 100%;
        border-radius: 2px;
        border: 2px solid $main-blue;
        font-family: Nunito;
        color: $neutral-black;
        font-weight: 300;
        background-color: $neutral-grey-pale;
        outline: 0;
        padding: .45rem .9rem .45rem 2.4rem;
        min-width: 300px;
        max-width: 500px;
    }

    .search-bar input:focus {
        border-radius: 4px;
        border: 4px solid $yellow-warm;
        font-family: Nunito;
        color: $neutral-black;
        font-weight: 700;

        + .search-icon {
            color: $yellow-warm;
        }
    }

    .search-bar .search-icon {
        position: absolute;
        top: 50%;
        left: 12px;
        font-size: 18px;
        transform: translateY(-50%);
        color: $main-blue;
    }

    #patientsResults {
        list-style: none;
        position: relative;
        border: 1px solid $neutral-black;
        overflow-y: auto;
        overflow-x: hidden;
        display: block;
        max-height: 200px;
        width: 100%;
        padding: 0;
        border-radius: 1px;
        margin: auto;
        margin-top: 2px;
        min-width: 300px;
        max-width: 500px;

        &:empty {
            display: none;
        }

        li {
            position: relative;
            border-bottom: 0.5px solid $neutral-black;
            background-color: $neutral-grey-pale;
            text-align: left;
            cursor: pointer;
            padding: .5em;

            i {
                position: absolute;
                right: 40px;
                top: 50%;
                transform: translateY(-50%);
                font-size: 20px;
                border: 1px solid $neutral-black;
                background: $neutral-white;
            }

            .label {
                color: $nav-text;
            }
        }

        li:last-child {
            border-bottom: none;
        }

        li:hover {
            background: $neutral-grey-mid;
        }
    }

    @media screen and (max-width: 430px) {
        .search-bar input {
            width: 100%;
        }

        #patientsResults {
            width: 100%;
        }

        .search-bar .search-icon {
            right: auto;
        }
    }

</style>
