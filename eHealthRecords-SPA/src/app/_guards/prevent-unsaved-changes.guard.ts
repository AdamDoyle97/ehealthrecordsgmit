import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberEditComponent } from '../members/member-edit/member-edit.component';

@Injectable()
export class PreventUnsavedChanges implements CanDeactivate<MemberEditComponent> {
    // if user changes pages while in editor mode message prompt to save changes
    canDeactivate(component: MemberEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Unsaved changes, are you sure you want to continue?');
        }
        return true;
    }
}
