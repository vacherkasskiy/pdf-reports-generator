import styles from "./NewReportInput.module.scss"
import React from "react";
import {MyButton} from "@/ui";
import {
    Formik,
    Form,
    Field,
} from 'formik';
import CreateReportRequest from "../../../api/requests/createReportRequest";

interface NewReportInputProps {
    onAdd: (report: CreateReportRequest) => void
    isInProgress: boolean
    isSuccess: boolean
    isError: boolean
}

function NewReportInput(
    {
        onAdd,
        isInProgress,
        isSuccess,
        isError
    }: NewReportInputProps): React.ReactElement {

    return (
        <div className={styles.newReportInput}>
            <Formik
                initialValues={{reportName: '', authorName: '', reportBody: ''} as CreateReportRequest}
                onSubmit={(values) => {
                    onAdd(values);
                }}
            >
                {() => (
                    <Form>
                        <label htmlFor="reportName">Report Name</label>
                        <Field id="reportName" name="reportName" placeholder="Report Name"/>

                        <label htmlFor="authorName">Author Name</label>
                        <Field id="authorName" name="authorName" placeholder="Author Name"/>

                        <label htmlFor="reportBody">Report Body</label>
                        <Field id="reportBody" name="reportBody" placeholder="Report Body"/>

                        <MyButton
                            typeProperty='submit'
                            text={'SEND'}
                            theme={'green'}
                            disabled={isInProgress}
                        />
                    </Form>
                )}
            </Formik>

            {isSuccess ? (
                <div className={`${styles.message} ${styles.success}`}>
                    <p className={styles.text}>Report successfully started generating!</p>
                </div>
            ) : ''}
            {isError ? (
                <div className={`${styles.message} ${styles.error}`}>
                    <p className={styles.text}>Something went wrong.</p>
                </div>
            ) : ''}

        </div>
    )
}

export default NewReportInput;