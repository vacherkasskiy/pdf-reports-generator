import styles from "./NewReportInput.module.scss"
import React, {useRef} from "react";
import {MyButton} from "@/ui";

interface NewReportInputProps {
    text: string | undefined
    onAdd: () => void
    onChange: (reportBody: string | undefined) => void
    isInProgress: boolean
    isSuccess: boolean
    isError: boolean
}

function NewReportInput({text, onAdd, onChange, isInProgress, isSuccess, isError}: NewReportInputProps): React.ReactElement {
    const textarea = useRef<HTMLTextAreaElement>(null)

    return (
        <div className={styles.newReportInput}>
            <textarea
                ref={textarea}
                className={styles.textarea}
                onChange={() => onChange(textarea.current?.value)}
                value={text ?? ""}
            ></textarea>
            <div className={styles.sendButtonContainer}>
                <MyButton
                    text={'SEND'}
                    theme={'green'}
                    onClick={onAdd}
                    disabled={isInProgress}
                />
            </div>
            {isSuccess ? (
                <div className={`${styles.message} ${styles.success}`}>
                    <p className={styles.text}>Task successfully started generating!</p>
                </div>
            ) : ''}
            {isError ? (
                <div className={`${styles.message} ${styles.error}`}>
                    <p className={styles.text}>Something wrong happened.</p>
                </div>
            ) : ''}
        </div>
    )
}

export default NewReportInput;