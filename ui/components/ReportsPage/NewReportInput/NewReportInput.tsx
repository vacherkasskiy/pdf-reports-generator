import styles from "./NewReportInput.module.scss"
import React, {useRef} from "react";
import {MyButton} from "@/ui";

interface NewReportInputProps {
    text: string | undefined
    onAdd: () => void
    onChange: (reportBody: string | undefined) => void
}

function NewReportInput({text, onAdd, onChange}: NewReportInputProps): React.ReactElement {
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
                />
            </div>
        </div>
    )
}

export default NewReportInput;