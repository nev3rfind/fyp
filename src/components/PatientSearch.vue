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
                            <input id="searchBar" type="text" autocomplete="off" class="form-control input-icon" placeholder="Search">
                            <i class='bx bx-search-alt search-icon'></i>
                        </div>
                        <ul id="patientsResults">
                            <li>Patient A <i class='bx bxs-lock-open text-success'></i></li>
                            <li>Patient A <i class='bx bxs-lock text-danger'></i></li>
                            <li>Patient A</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>

</script>

<style lang="scss" scoped>
    @import "../sass/app.scss";

    .center-content {
        height: 100vh;
        display: flex;
        align-items: center;
        justify-content: center;
        padding-bottom: 50%;
    }

    .title-container {
        width: 100%;
        max-width: 400px;

        h1 {
            margin-bottom: 30px;
        }
    }


    .search-bar {
        .input-container {
            position: relative;
        }

        input {
            border-radius: 2px;
            border: 2px solid $main-blue;
            font-family: Nunito;
            color: $neutral-black;
            font-weight: 300;
            background-color: $neutral-grey-pale;
            outline: 0;
            padding: .45rem .9rem .45rem 2.4rem;
        }

        input:focus {
            border-radius: 4px;
            border: 4px solid $yellow-warm;
            font-family: Nunito;
            color: $neutral-black;
            font-weight: 700;

            + .search-icon {
                color: $yellow-warm;
            }
        }

        .search-icon {
            position: absolute;
            top: 50%;
            left: 12px;
            font-size: 18px;
            transform: translateY(-50%);
            color: $main-blue;
        }
    }

    #patientsResults {
        list-style: none;
        position: relative;
        border: 1px solid $neutral-black;
        border-bottom: none;
        overflow-y: auto;
        overflow-x: hidden;
        display: block;
        max-height: 200px;
        // margin: 0 0.5em;
        padding: 0;
        border-radius: 1px;
        margin: auto;
        margin-top: 2px;

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

        li:hover {
            background: $neutral-grey-mid;
        }
    }

    @media screen and (max-width: 430px) {
        .search-bar input {
            width: 100%;
        }

        #countriesResults {
            width: 100%;
        }

        .search-bar .search-icon {
            right: auto;
        }
    }

</style>
