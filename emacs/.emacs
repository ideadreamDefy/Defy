(add-to-list 'load-path' "~/.emacs.d/_emacs/")
(add-to-list 'custom-theme-load-path "~/.emacs.d/themes/")


;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;彩虹猫动画;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;;;彩虹猫动画
(load-file "~/.emacs.d/nyan-mode/nyan-mode.el")

;;彩虹猫做动画
(setq default-mode-line-format  
      (list ""  
            'mode-line-modified  
            "<"  
            "kirchhoff"  
            "> "  
            "%10b"  
            '(:eval (when nyan-mode (list (nyan-create))));;注意添加此句到你的format配置列表中  
            " "  
            'default-directory  
            " "  
            "%[("  
            'mode-name  
            'minor-mode-list  
            "%n"  
            'mode-line-process  
            ")%]--"  
            "Line %l--"  
            '(-1 . "%P")  
            "-%-"))  
(nyan-mode t);;启动nyan-mode  
(nyan-start-animation)
;;开始动画
;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
;; 关闭工具栏
(tool-bar-mode 0)

;;防止页面滚动时跳动，
;;可以在靠近屏幕边沿3行时就开始滚动
scroll-margin 3
;;scroll-step 1 设置为每次翻滚一行，可以使页面更连续
(setq scroll-step 1 scroll-margin 3 scroll-conservatively 10000)

;;指定目下全局搜索
(global-set-key (kbd "C-f") 'rgrep)


;;set transparent effect
(global-set-key [(f1)] 'loop-alpha)
(setq alpha-list '((100 100) (95 65) (85 55) (75 45) (65 35)))
(defun loop-alpha ()
  (interactive)

  (let ((h (car alpha-list)))                ;; head value will set to
    ((lambda (a ab)
       (set-frame-parameter (selected-frame) 'alpha (list a ab))
       (add-to-list 'default-frame-alist (cons 'alpha (list a ab)))
       ) (car h) (car (cdr h)))
    (setq alpha-list (cdr (append alpha-list (list h))))
    )
    (message "switch transparent successfully")
  )

;;格式化整个文件函数
(defun indent-whole ()
  (interactive)
  (indent-region (point-min) (point-max))
  (message "format successfully"))
;;绑定到F2键
(global-set-key [f2] 'indent-whole)

;;文件搜索
(global-set-key [f3] 'find-name-dired)

;;搜索文件中相同的字符串
(global-set-key [f4] 'list-matching-lines)

;;刷新buffer
(defun refresh-file ()  
  (interactive)  
  (revert-buffer t (not (buffer-modified-p)) t)
  (message "refresh buffer successfully")
)

(global-set-key [f5] 'refresh-file) 

;;eshell 开启
(global-set-key [f6] 'eshell)

;;shell 开启
(global-set-key [f7] 'shell)

					;设置TAB默认的宽度
(setq default-tab-width 4)
;;选中注释
(defun qiang-comment-dwim-line (&optional arg)
  "Replacement for the comment-dwim command. If no region is selected and current line is not blank and we are not at the end of the line, then comment current line. Replaces default behaviour of comment-dwim, when it inserts comment at the end of the line."
  (interactive "*P")
  (comment-normalize-vars)
  (if (and (not (region-active-p)) (not (looking-at "[ \t]*$")))
      (comment-or-uncomment-region (line-beginning-position) (line-end-position))
    (comment-dwim arg)))
(global-set-key (kbd "C-/")'qiang-comment-dwim-line)

;; 显示行号
(global-linum-mode 1)

(load-theme 'molokai t)

(set-default-font "Comic Sans MS Bold 14")

(global-hl-line-mode 2)



;; From emacs-wiki:
(defun shade-color (intensity)
  "print the #rgb color of the background, dimmed according to intensity"
  (interactive "nIntensity of the shade : ")
  (apply 'format "#%02x%02x%02x"
         (mapcar (lambda (x)
                   (if (> (lsh x -8) intensity)
                       (- (lsh x -8) intensity)
                     0))
                 (color-values (cdr (assoc 'background-color (frame-parameters)))))))

;; Default hl
(global-hl-line-mode 1)

(set-face-background 'hl-line "#7D26CD")

(set-face-foreground 'highlight nil)


;; (make-variable-buffer-local 'global-hl-line-mode)
;; (set-face-background hl-line-face (shade-color 08))  

;; (defface hl-line-highlight-face
;;   '((t :inherit highlight))
;;   "Face for highlighting the current line with `hl-line-fancy-highlight'."
;;   :group 'hl-line)

;; (defun hl-line-fancy-highlight ()
;;   (set (make-local-variable 'hl-line-face) 'hl-line-highlight-face)
;;   ;;    (set (make-local-variable 'line-move-visual) nil)
;;   ;;    (set (make-local-variable 'cursor-type) nil)
;;   (setq global-hl-line-mode nil)
;;   (hl-line-mode))

;; (add-hook 'org-agenda-mode-hook 'hl-line-fancy-highlight)
;; (add-hook 'gnus-summary-mode-hook 'hl-line-fancy-highlight)
;; (add-hook 'gnus-group-mode-hook 'hl-line-fancy-highlight)

;; (set-hl-line-color "white")
;; highlighting



;;------------显示时间设置------------------------------

(display-time-mode 1);;启用时间显示设置，在minibuffer上面的那个杠上
(setq display-time-24hr-format t);;时间使用24小时制
(setq display-time-day-and-date t);;时间显示包括日期和具体时间
(setq display-time-use-mail-icon t);;时间栏旁边启用邮件设置
(setq display-time-interval 10);;时间的变化频率，单位多少来着？

(show-paren-mode t)
;;打开括号匹配显示模式
