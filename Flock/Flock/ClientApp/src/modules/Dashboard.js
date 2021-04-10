﻿import React, { useEffect, useState } from 'react';

import getCampaigns, { getDashboardCampaigns } from '../dataRequests/getCampaigns';
import getAccount from '../dataRequests/getAccount';
import editCampaign from '../dataRequests/editCampaign';

import dataToAccordionConvert from '../usefulFunctions/dataToAccordionItemsConvert';
import { newContactFormInputs } from '../usefulFunctions/formInputs';
import csvToJson from '../usefulFunctions/csvToJson';

import Accordion from '../components/Accordion';
import Form from '../components/Form';

import '../modulesCSS/Dashboard.css'

const inputsBasicInfo =
    [
        {
            label: "Number of contacts",
            id: "contacts",
            readOnly: true
        },
        {
            label: "Emails sent",
            id: "emails",
            readOnly: true
        },
        {
            label: "Active campaigns",
            id: "campaigns",
            readOnly: true
        }
    ]



const Dashboard = () => {

    const [campaigns, setCampaigns] = useState([]);
    const [basicInfo, setBasicInfo] = useState({
        numOfConts: 0,
        numOfCamps: 0,
        numOfSent: 0
    })

    useEffect(() => {
        setCampaigns(getDashboardCampaigns());
        setBasicInfo(() => {
            const { numOfConts, numOfCamps, numOfSent } = getAccount();
            return { numOfConts, numOfCamps, numOfSent };
        });
    },[])

    const inputsBasicInfo =
        [
            {
                label: "Number of contacts",
                id: "contacts",
                readOnly: true,
                value: basicInfo["numOfConts"]
            },
            {
                label: "Emails sent",
                id: "emails",
                readOnly: true,
                value: basicInfo["numOfCamps"]
            },
            {
                label: "Active campaigns",
                id: "campaigns",
                readOnly: true,
                value: basicInfo["numOfSent"]
            }
        ]

    const accordion = dataToAccordionConvert(campaigns, editCampaign);

    const contactNum = 50;
    const campaignsNum = 10;
    const emailsNum = 250;

    const createContacts = () => {

        if (document.querySelector("#import").files[0])
            csvToJson(document.querySelector("#import").files[0]);

    }

    return (
        <div className="Dashboard">

            <div className="flex-item">
                <div className="ui segment import ">
                    <h1>Hello again **username**</h1>
                    <label className="custom-file-upload">
                        <input type="file" id="import" />
                            Custom Upload
                        </label>
                    <button onClick={createContacts}>AAA</button>
                    
                </div>
            </div>

            <div className="inner-flex flex-item">

                <div className="inner-inner-flex flex-item">


                    <div className="ui segment new-group flex-item">
                        <h1>Create a new group!</h1>
                        <button className="ui button">Create group</button>
                    </div>




                    <Form
                        className="flex-item"
                        label="Basic info"
                        inputs={inputsBasicInfo}
                    />



                </div>



                <Form
                    className="flex-item"
                    label="Add a new contact!"
                    inputs={newContactFormInputs}
                    submit={{ label: "submit", onClick: () => console.log("submited") }}
                />




                <div className="ui segment campaigns flex-item">
                    <h1>Campaign Information</h1>
                    <Accordion items={accordion} />
                    <button className="ui button">New Campaign</button>
                </div>


            </div>



        </div>
    );
}

export default Dashboard;