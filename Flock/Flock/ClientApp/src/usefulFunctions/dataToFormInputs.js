﻿/*gets data in the form:
 * {
        title: "Stefanos",
        details: {
            name: "Stefanos",
            surname: "Toufexis",
            email:"stefetoufe@gmail.com"
        },
        id: "1"
    },
    {
        title: "Thanos",
        details: {
            name: "Stefanos",
            surname: "Toufexis",
            email: "stefetoufe@gmail.com"
        },
        id: "2"
    },

    And converts it into an accordion
 * */



const dataToFormInputs = (details) => {


        const inputs = Object.keys(details).map((det) => {

            const type = det === "email" ? "email" : "text"; //used for validation

            return {
                label: det,
                id: det,
                value: details[det],
                type:type
            }
        });


    return inputs;

}

export default dataToFormInputs;